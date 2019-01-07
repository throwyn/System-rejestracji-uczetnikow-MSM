using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SRUK.Models;
using SRUK.Entities;
using SRUK.Models.AccountViewModels;
using SRUK.Services;
using SRUK.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.IO;

namespace SRUK.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ReviewsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPaperRepository _paperRepository;
        private readonly IPaperVersionRepository _paperVersionRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ISeasonRepository _seasonRepository;

        public ReviewsController(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            IPaperRepository paperRepository,
            IPaperVersionRepository paperVersionRepository,
            IReviewRepository reviewRepository,
            ISeasonRepository seasonRepository
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _paperRepository = paperRepository;
            _paperVersionRepository = paperVersionRepository;
            _reviewRepository = reviewRepository;
            _seasonRepository = seasonRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        //GET: Reviews
        public ActionResult Index(
            int currentPage = 1,
            short sortBy = 0,
            string title = "",
            string firstNameAuthor = "",
            string lastNameAuthor = "",
            string firstNameCritic = "",
            string lastNameCritic = "",
            string status = "")
        {

            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var filteredList = _reviewRepository.GetFilteredReviews(
             sortBy, title, firstNameAuthor,lastNameAuthor,firstNameCritic,lastNameCritic, status, 10, currentPage);

            var model = new ReviewIndexViewModel
            {
                FirstNameAuthor = firstNameAuthor,
                LastNameAuthor = lastNameAuthor,
                FirstNameCritic = firstNameCritic,
                LastNameCritic = lastNameCritic,
                Title = title,
                Status = status,

                Results = filteredList.Results,
                CurrentPage = filteredList.CurrentPage,
                PageCount = filteredList.PageCount,
                PageSize = filteredList.PageSize,
                RecordCount = filteredList.RecordCount,
                StatusMessage = StatusMessage
            };

            ViewBag.Statuses = GetReviewsStatusesSelectListItem(status);
            ViewBag.SortBy = GetReviewsSortBySelectListItem(sortBy);
            return View(model);

        }

        // GET: Reviews/Details/5
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var review = _reviewRepository.GetReview(id);

            if (review == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction("Index", "Home");
            }

            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var model = Mapper.Map<ReviewDetailsViewModel>(review);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // GET: Reviews/MyReview/5
        [Route("MyReview/{id}")]
        public async Task<IActionResult> MyReview(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var review = _reviewRepository.GetReview(id);

            if (review == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction("Index", "Home");
            }

            if (review.CriticId != user.Id)
                return RedirectToAction("Index", "Home");

            var model = Mapper.Map<ReviewDetailsViewModel>(review);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // GET: Reviews/Review/5
        [Route("Review/{id}")]
        public async Task<IActionResult> Review(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var review = _reviewRepository.GetReview(id);

            if (review == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction("Index", "Home");
            }

            if (user.Id != review.PaperVersion.Paper.Participancy.User.Id)
                return RedirectToAction("Index", "Home");

            var model = Mapper.Map<ReviewViewModel>(review);
            model.StatusMessage = StatusMessage;
            return View(model);
        }
        // GET: AddCritic/{id}
        [HttpGet]
        [Route("AddCritic/{id}")]
        public IActionResult AddCritic(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var version = _paperVersionRepository.GetPaperVersion(id);

            if (version == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction("Index", "PaperVersions");
            }
            var users = _userRepository.GetAdminsAndCritics();
            ViewBag.Critics = new List<SelectListItem>();
            foreach (var user in users)
            {
                ViewBag.Critics.Add(new SelectListItem { Text = user.Degree + " " + user.FirstName + " " + user.LastName + " (" + user.Email + ')', Value = user.Id });
            };

            var model = new AddCriticViewModel()
            {
                PaperVersion = version,
                PaperVersionId = version.Id,
                StatusMessage = StatusMessage
            };
            return View(model);
        }

        // POST: AddCritic/{id}
        [HttpPost]
        [Route("AddCritic/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult AddCritic(AddCriticViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {

                var version = _paperVersionRepository.GetPaperVersion(model.PaperVersionId);
                if (version == null)
                {
                    StatusMessage = "Error. Paper do not exists.";
                    return RedirectToAction("Index", "PaperVersions");
                }
                var critic = _userRepository.GetApplicationUser(model.CriticId);
                if (!_userManager.IsInRoleAsync(critic, "Critic").Result && !_userManager.IsInRoleAsync(critic, "Admin").Result)
                {
                    StatusMessage = "Error. This user cannot become a  critic!";
                    return RedirectToAction("Index", "PaperVersions");
                }
                if(version.Reviews.FirstOrDefault(r => r.CriticId == critic.Id) != null)
                {
                    StatusMessage = "Error. "+critic.FirstName+" " +critic.LastName +" is already assigned to this version!";
                    var a = version.Reviews.Where(r => r.CriticId == critic.Id);
                    return RedirectToAction("AddCritic", "Reviews", new { Id = model.PaperVersionId });
                }
                
                var review = new ReviewDTO
                {
                    CriticId = model.CriticId,
                    PaperVersionId = model.PaperVersionId,
                    Deadline = model.Deadline
                };
                
                var result = _reviewRepository.CreateReview(review);
                if (result == 1)
                {
                    _paperVersionRepository.SetStatusWaitingForReview(model.PaperVersionId);

                    StatusMessage = "Critic has been choosen.";
                    return RedirectToAction("Index", "PaperVersions");
                }

            }
            StatusMessage = "Error. Entered data is not valid.";
            return RedirectToAction("Index", "PaperVersions");
        }

        // GET: ChooseCritic/{id}
        [HttpGet]
        [Route("ChooseCritics/{id}")]
        public IActionResult ChooseCritics(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var version = _paperVersionRepository.GetPaperVersion(id);

            if (version == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction("Index", "PaperVersions");
            }
            var users = _userRepository.GetAdminsAndCritics();
            ViewBag.Critics = new List<SelectListItem>();
            foreach (var user in users)
            {
                ViewBag.Critics.Add(new SelectListItem { Text = user.Degree+" "+user.FirstName+" "+user.LastName+" ("+user.Email+')', Value = user.Id });
            };

            var model = new ChooseCriticsViewModel()
            {
                PaperVersion = version,
                PaperVersionId = version.Id,
                StatusMessage = StatusMessage
            };
            return View(model);
        }

        // POST: ChooseCritic/{id}
        [HttpPost]
        [Route("ChooseCritics/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseCritics(ChooseCriticsViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {

                var version = _paperVersionRepository.GetPaperVersion(model.PaperVersionId);
                if (version == null)
                {
                    StatusMessage = "Error. Paper do not exists.";
                    return RedirectToAction("Index", "PaperVersions");
                }
                if (model.FirstCriticId == model.SecondCriticId)
                {
                    StatusMessage = "Error. Two different critics should be chosen!";
                    return RedirectToAction("ChooseCritics", model.PaperVersionId);
                }
                var firstCritic = _userRepository.GetApplicationUser(model.FirstCriticId);
                var secondCritic = _userRepository.GetApplicationUser(model.SecondCriticId);
                if (!_userManager.IsInRoleAsync(firstCritic, "Critic").Result && !_userManager.IsInRoleAsync(firstCritic, "Admin").Result)
                {
                    StatusMessage = "Error. First user cannot become a  critic!";
                    return RedirectToAction("Index", "PaperVersions");
                }
                if (!_userManager.IsInRoleAsync(secondCritic, "Critic").Result && !_userManager.IsInRoleAsync(secondCritic, "Admin").Result)
                {
                    StatusMessage = "Error. Second user cannot become a  critic!";
                    return RedirectToAction("Index", "PaperVersions");
                }

                List<ReviewDTO> list = new List<ReviewDTO>();
                list.Add(new ReviewDTO
                {
                    CriticId = model.FirstCriticId,
                    PaperVersionId = model.PaperVersionId,
                    Deadline = model.Deadline
                }
                );
                list.Add(new ReviewDTO
                {
                    CriticId = model.SecondCriticId,
                    PaperVersionId = model.PaperVersionId,
                    Deadline = model.Deadline
                });

                IEnumerable<ReviewDTO> reviews = list;

                var result = _reviewRepository.CreateReviews(reviews);
                if (result == 2)
                {
                     _paperVersionRepository.SetStatusWaitingForReview(model.PaperVersionId);
                    
                    StatusMessage = "Critics has been choosen.";
                    return RedirectToAction("Index", "PaperVersions");
                }

            }
            StatusMessage = "Error. Entered data is not valid.";
            return RedirectToAction("Index", "PaperVersions");
        }

        // GET: Reviews/Download/5
        [Route("Download/{id}")]
        public async Task<IActionResult> Download(long id)
        {
            var review =  _reviewRepository.GetReview(id);

            if (review == null)
            {
                StatusMessage = "Error. This review do not exists.";
                return RedirectToAction("MyPapers", "Papers");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (review.CriticId != user.Id && !User.IsInRole("Admin"))
            {
                StatusMessage = "Error. You don't have permission to do that.";
                return RedirectToAction("MyPapers", "Papers");
            }

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\Reviews", review.FileName);
            if (!System.IO.File.Exists(path))
            {
                StatusMessage = "Error. This file don't exists.";
                return RedirectToAction("Index", "Home");
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), review.OriginalFileName);
        }



        // GET: Reviews/AddReview/5
        [HttpGet]
        [Route("AddReview/{id}")]
        public async Task<IActionResult> AddReview(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var review = _reviewRepository.GetReview(id);
            if (user.Id != review.CriticId)
            {
                StatusMessage = "Error. You cannot review this file.";
                return RedirectToAction(nameof(MyReviews));
            }

            var model = Mapper.Map<AddReviewViewModel>(review);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: Reviews/AddReview
        [HttpPost]
        [Route("AddReview/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var existingReview = _reviewRepository.GetReview(model.Id);
            if (user.Id != existingReview.CriticId)
            {
                StatusMessage = "Error. You cannot review this file.";
                return RedirectToAction(nameof(MyReviews));
            }
            if (existingReview.FileName != null)
            {
                StatusMessage = "Error. You can review this paper only once.";
                return RedirectToAction(nameof(MyReviews));
            }
            if (model.File == null || model.File.Length == 0)
            {
                StatusMessage = "Error. File is missing or broken.";
                return View(model);
            }
            if (!model.File.FileName.EndsWith(".doc") && !model.File.FileName.EndsWith(".docx") && !model.File.FileName.EndsWith(".pdf") && !model.File.FileName.EndsWith(".odt"))
            {
                StatusMessage = "Error. File has forbidden extension.(Only .doc .docx .pdf .odt allowed)";
                return RedirectToAction("AddReview", new { id = model.Id });
            }
            if (ModelState.IsValid)
            {
                string newFileName = Guid.NewGuid().ToString() + model.File.FileName.Substring(model.File.FileName.Length - 4);
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Reviews",
                        newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                var review = Mapper.Map<ReviewDTO>(model);
                review.OriginalFileName = model.File.FileName.Split('\\').Last();
                review.FileName = newFileName;
                review.CompletionDate = DateTime.Now;


                var result = _reviewRepository.AddReview(review);

                if (result == 1)
                {
                    var reviews = _reviewRepository.GetPaperVersionReviews(existingReview.PaperVersionId);
                    if (reviews.Where(r => r.Recommendation == 2).Count() >= 2)
                    {
                        _paperVersionRepository.SetStatusVersionAccepted(existingReview.PaperVersionId);
                        _paperRepository.SetStatuAccepted(existingReview.PaperVersion.PaperId);
                    }
                    else if (reviews.Where(r => r.Recommendation == 5).Count() >= 2)
                    {
                        _paperVersionRepository.SetStatusVersionRejected(existingReview.PaperVersionId);
                        _paperRepository.SetStatusDiscarded(existingReview.PaperVersion.PaperId);
                    }
                    else if (reviews.Where(r => r.Recommendation == 3).Count() >= 2)
                         _paperVersionRepository.SetStatusMinorRevision(existingReview.PaperVersionId);
                    else if (reviews.Where(r => r.Recommendation == 4).Count() >= 2)
                         _paperVersionRepository.SetStatusMajorRevision(existingReview.PaperVersionId);
                    else if (reviews.Where(r => r.Recommendation == 3).Count() >= 1 && reviews.Where(r => r.Recommendation == 2).Count() >= 1)
                        _paperVersionRepository.SetStatusMinorRevision(existingReview.PaperVersionId);
                    else if (reviews.Where(r => r.Recommendation == 4).Count() >= 1 && (reviews.Where(r => r.Recommendation == 2 || r.Recommendation == 3).Count() >= 1))
                        _paperVersionRepository.SetStatusMajorRevision(existingReview.PaperVersionId);
                    else if(reviews.Where(r => r.Recommendation > 1).Count() == 2 && reviews.Where(r => r.Recommendation == 5).Count() == 1)
                    {
                        //wysłac info do admina o wyborze kolejnego 
                    }

                    StatusMessage = "Succesfully added.";
                    return RedirectToAction(nameof(MyReviews));
                }

                StatusMessage = "Error. If you see this error. Contact with administrator.";
                return RedirectToAction(nameof(MyReviews));
            }
            StatusMessage = "Error. Entered data is not valid.";
            return View(model);
        }

        // GET: Reviews/MyReviews
        [Route("MyReviews")]
        public async Task<ActionResult> MyReviews()
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Critic"))
                return RedirectToAction(nameof(Index), "Home");

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var reviews = _reviewRepository.GetUserReviews(user.Id).ToList();

            var model = new MyReviewsViewModel
            {
                Reviews = reviews,
                StatusMessage = StatusMessage
            };
            return View(model);

        }


        // GET: Reviews/CancelCritic/5
        [HttpGet]
        [Route("CancelCritic/{id}")]
        public IActionResult CancelCritic(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var review = _reviewRepository.GetReview(id);
            if (review == null)
            {

                StatusMessage = "Error. Review do not exists.";
                return RedirectToAction(nameof(Index));
            }

            var model = Mapper.Map<ReviewDetailsViewModel>(review);
            model.StatusMessage = StatusMessage;
            return View(model);
        }
        // POST: Reviews/CancelCritic/5
        [HttpPost]
        [Route("CancelCritic/{id}")]
        public IActionResult CancelCritic(ReviewDetailsViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            var result = _reviewRepository.RemoveReview(model.Id);
            if (result == 1)
            {
                var version = _paperVersionRepository.GetPaperVersion(model.PaperVersionId);
                if (version.Reviews.Count() == 0)
                {
                    _paperVersionRepository.SetStatusDocumentRecieved(model.PaperVersionId);
                }

                StatusMessage = "Critic canceled.";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/RejectReviewing/5
        [HttpGet]
        [Route("RejectReviewing/{id}")]
        public async Task<IActionResult> RejectReviewing(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var review = _reviewRepository.GetReview(id);
            if (review == null)
            {
                StatusMessage = "Error. Review do not exists.";
                return RedirectToAction(nameof(Index));
            }
            if(review.CriticId != user.Id)
            {
                StatusMessage = "Error. You cannot reject this!";
                return RedirectToAction(nameof(Index));
            }

            var model = Mapper.Map<ReviewDetailsViewModel>(review);
            model.StatusMessage = StatusMessage;
            return View(model);
        }
        // POST: Reviews/RejectReviewing/5
        [HttpPost]
        [Route("RejectReviewing/{id}")]
        public async Task<IActionResult> RejectReviewing(ReviewDetailsViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = _reviewRepository.RemoveReview(model.Id);
            if (result == 1)
            {
                var version = _paperVersionRepository.GetPaperVersion(model.PaperVersionId);
                if (version.Reviews.Count() == 0)
                {
                    _paperVersionRepository.SetStatusDocumentRecieved(model.PaperVersionId);
                }

                StatusMessage = "Critic canceled.";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(Index));
        }

        #region Helpers


        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".odt","application/vnd.oasis.opendocument.text" }
            };
        }

        private List<SelectListItem> GetSeasonsSelectListItem(string selectedValue)
        {

            var seasons = _seasonRepository.GetSeasons().ToList();

            var result = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Season", Value = "" }
            };

            foreach (var season in seasons)
            {
                result.Add(new SelectListItem { Text = season.EditionNumber, Value = season.Id.ToString() });
            }

            if (selectedValue != null && result.Where(r => r.Value == selectedValue).Count() > 0)
                result.FirstOrDefault(r => r.Value == selectedValue).Selected = true;

            return result;
        }

        private List<SelectListItem> GetReviewsStatusesSelectListItem(string selected)
        {

            var result = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Status", Value = "" },
                new SelectListItem { Text = "Created", Value = "0" },
                new SelectListItem { Text = "Cancelled", Value = "1" },
                new SelectListItem { Text = "Accepted", Value = "2" },
                new SelectListItem { Text = "Accepted with minor changes", Value = "3" },
                new SelectListItem { Text = "Major revision", Value = "4" },
                new SelectListItem { Text = "Rejected", Value = "5" }
            };
            if (selected != null)
                result.FirstOrDefault(r => r.Value == selected.ToString()).Selected = true;

            return result;
        }

        private List<SelectListItem> GetReviewsSortBySelectListItem(int? selected)
        {

            var result = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Sort by", Value = "" },
                new SelectListItem { Text = "Title Asc", Value = "1" },
                new SelectListItem { Text = "Title Desc", Value = "2" },
                new SelectListItem { Text = "Author name Asc", Value = "3" },
                new SelectListItem { Text = "Author name Desc", Value = "4" },
                new SelectListItem { Text = "Critic name Asc", Value = "5" },
                new SelectListItem { Text = "Critic name Desc", Value = "6" },
                new SelectListItem { Text = "Oldest first", Value = "7" },
                new SelectListItem { Text = "Latest first", Value = "8" }
            };
            if (selected > 0)
                result.FirstOrDefault(r => r.Value == selected.ToString()).Selected = true;

            return result;
        }



        #endregion

    }
}