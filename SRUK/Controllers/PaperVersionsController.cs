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
    public class PaperVersionsController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPaperRepository _paperRepository;
        private readonly IPaperVersionRepository _paperVersionRepository;
        private readonly IParticipanciesRepository _participanciesRepository;
        private readonly IReviewRepository _reviewRepository;

        public PaperVersionsController(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            IPaperRepository paperRepository,
            IPaperVersionRepository paperVersionRepository,
            IParticipanciesRepository participanciesRepository,
            IReviewRepository reviewRepository
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _paperRepository = paperRepository;
            _paperVersionRepository = paperVersionRepository;
            _participanciesRepository = participanciesRepository;
            _reviewRepository = reviewRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Papers
        public ActionResult Index(
            int currentPage = 1,
            short sortBy = 0,
            string title = "",
            string firstName = "",
            string lastName = "",
            string status = "")
        {

            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var filteredList = _paperVersionRepository.GetFilteredVersions(
             sortBy, title, firstName, lastName, status, 10, currentPage);

            var model = new PaperVersionIndexViewModel
            {
                Title = title,
                FirstName = firstName,
                LastName = lastName,
                Status = status,

                Results = filteredList.Results,
                CurrentPage = filteredList.CurrentPage,
                PageCount = filteredList.PageCount,
                PageSize = filteredList.PageSize,
                RecordCount = filteredList.RecordCount,
                StatusMessage = StatusMessage
            };

            ViewBag.Statuses = GetVersionsStatusesSelectListItem(status);
            ViewBag.SortBy = GetVersionsSortBySelectListItem(sortBy);
            return View(model);

        }


        // GET: PaperVersions/Add
        [Route("Add/{id}")]
        public async Task<IActionResult> Add(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var paper = _paperRepository.GetPaper(id);

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction("MyPapers", "Papers");
            }
            var participancy = _participanciesRepository.GetParticipancy(paper.ParticipancyId);
            if (participancy.User.Id != user.Id)
            {
                StatusMessage = "Error. You cannot add version of this paper.";
                return RedirectToAction("MyPapers", "Papers");
            }

            var model = new PaperVersionsAddViewModel()
            {
                PaperId = paper.Id,
                StatusMessage = StatusMessage
            };
            return View(model);
        }

        // POST: PaperVersions/Add
        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVersion(PaperVersionsAddViewModel model)
        {
            if (model == null)
            {
                StatusMessage = "Error. Something went wrong.";
                return View();
            }
            if (model.File == null || model.File.Length == 0)
            {
                StatusMessage = "Error. File is missing or broken.";
                return View(model);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var paper = _paperRepository.GetPaper(model.PaperId);

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction("MyPapers", "Papers");
            }

            if (paper.Status != 1)
            {
                StatusMessage = "Error. You cannot add new version.";
                return RedirectToAction("MyPapers", "Papers");
            }
            var participancy = _participanciesRepository.GetParticipancy(paper.ParticipancyId);
            if (paper.Participancy.User.Id != user.Id)
            {
                StatusMessage = "Error. You cannot add version of this paper.";
                return RedirectToAction("MyPapers", "Papers");
            }

            if (!model.File.FileName.EndsWith(".doc") && !model.File.FileName.EndsWith(".docx") && !model.File.FileName.EndsWith(".pdf") && !model.File.FileName.EndsWith(".odt"))
            {
                StatusMessage = "Error. File has forbidden extension.(Only .doc .docx .pdf .odt allowed)";
                return RedirectToAction("Add",new { id = model.PaperId });
            }
            if (ModelState.IsValid)
            {
                string newFileName = Guid.NewGuid().ToString() + model.File.FileName.Substring(model.File.FileName.Length - 4);
                var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Papers",
                        newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                var paperVersion = new PaperVersionDTO
                {
                    PaperId = model.PaperId,
                    OriginalFileName = model.File.FileName.Split('\\').Last(),
                    FileName = newFileName,
                    Status = 0
                };
                var newVersionId = _paperVersionRepository.AddPaperVersion(paperVersion);
                paper = _paperRepository.GetPaper(paper.Id);

                var differentJustCreatedVersion = paper.PaperVersions.FirstOrDefault(v => v.Status == 0 && v.Id != newVersionId);
                var versionWithMinorChanges = paper.PaperVersions.FirstOrDefault(v => v.Status == 4);
                var pastVersionsWithReviews = paper.PaperVersions.Where(v => (v.Status == 4 || v.Status == 5) && v.Reviews.Count() >= 2).OrderByDescending(v=>v.CreationDate);
                if (differentJustCreatedVersion != null)
                    _paperVersionRepository.SetStatusVersionRejected(differentJustCreatedVersion.Id);
                else if (versionWithMinorChanges != null)
                {
                    _paperVersionRepository.SetStatusVersionAccepted(newVersionId);
                    _paperRepository.SetStatuAccepted(paper.Id);
                }
                if(pastVersionsWithReviews.Count() >= 1)
                {
                    var lastReviews = pastVersionsWithReviews.First().Reviews.Where(r => r.Recommendation != 5 && r.Recommendation != 1);
                    foreach(var critic in lastReviews)
                    {
                        var review = new ReviewDTO
                        {
                            CriticId = critic.CriticId,
                            PaperVersionId = newVersionId,
                            Deadline = DateTime.Now.AddMonths(1)
                        };
                        _reviewRepository.CreateReview(review);
                    }
                    _paperVersionRepository.SetStatusWaitingForReview(newVersionId);
                }

                StatusMessage = "Version has beed added.";
                return RedirectToAction("MyPaper", "Papers", new { id = model.PaperId });
            }
            StatusMessage = "Error. Entered data is not valid.";
            return RedirectToAction("MyPapers", "Papers");
        }

        // GET: PaperVersions/Download/5
        [Route("Download/{id}")]
        public async Task<IActionResult> Download(long id)
        {
            PaperVersionDTO paperVersion =  _paperVersionRepository.GetPaperVersion(id);

            if (paperVersion == null)
            {
                StatusMessage = "Error. This version do not exists.";
                return RedirectToAction("MyPapers", "Papers");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (paperVersion.Paper.Participancy.User.Id != user.Id && !User.IsInRole("Admin") && paperVersion.Reviews.FirstOrDefault(r=>r.CriticId == user.Id) == null)
            {
                StatusMessage = "Error. You don't have permission to do that.";
                return RedirectToAction("MyPapers", "Papers");
            }

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\Papers", paperVersion.FileName);
            if (!System.IO.File.Exists(path))
            {
                StatusMessage = "Error. This file don't exists.";
                return RedirectToAction("MyPaper", "Papers", new { id = paperVersion.PaperId });
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), paperVersion.OriginalFileName);
        }

        // GET: PaperVersions/Delete/5
        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            var paper = _paperVersionRepository.GetPaperVersion(id);

            if (paper == null)
            {
                StatusMessage = "Error. Version do not exists.";
                return RedirectToAction("MyPapers", "Papers");
            }
            if(paper.Status != 0)
            {
                StatusMessage = "Error. You can't delete this version now. Contact with administrator, if you really want to.";
                return RedirectToAction("MyPaper", "Papers", new { id = paper.Id });
            }

            var model = Mapper.Map<PaperVersionDeleteViewModel>(paper);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: PaperVersions/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            PaperVersionDTO paperVersion = _paperVersionRepository.GetPaperVersion(id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            if (paperVersion.Paper.Participancy.User.Id != user.Id && !User.IsInRole("Admin"))
            {
                StatusMessage = "Error. Access denied.";
                return RedirectToAction("MyPaper", "Papers", new { id = paperVersion.PaperId });
            }

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "Files\\Papers", paperVersion.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var result =  _paperVersionRepository.DeleteVersion(id);
            if (result == 1)
            {
                StatusMessage = "Succesfully deleted.";
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Details", "Papers", new { id = paperVersion.PaperId });
                }
                return RedirectToAction("MyPaper", "Papers", new { id = paperVersion.PaperId });
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction("MyPapers", "Papers");
        }
        // GET: PaperVersions/Discard/5/
        [HttpGet]
        [Route("Discard/{id}")]
        public IActionResult Discard(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            PaperVersionDTO paperVersion = _paperVersionRepository.GetPaperVersion(id);

             _paperVersionRepository.SetStatusVersionRejected(id);
            var result =  _paperRepository.SetStatusDiscarded(paperVersion.PaperId);
            if (result == 1)
            {
                StatusMessage = "Succesfully discarded.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PaperVersions/RejectVersion/5
        [HttpGet]
        [Route("RejectVersion/{id}")]
        public async Task<IActionResult> RejectVersion(long id)
        {
            PaperVersionDTO paperVersion = _paperVersionRepository.GetPaperVersion(id);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (paperVersion == null)
            {
                StatusMessage = "Error. Version do not exists.";
                return RedirectToAction("MyPapers", "Papers");
            }
            if (paperVersion.Status != 0 && paperVersion.Status !=4)
            {
                StatusMessage = "Error. You can't reject this version now.";
                return RedirectToAction("Index", "PaperVersions");
            }

            var model = Mapper.Map<PaperVersionsRejectViewModel>(paperVersion);
            return View(model);
        }

        // POST: PaperVersions/RejectVersion/5
        [HttpPost]
        [Route("RejectVersion/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectVersionConfirmed(PaperVersionsRejectViewModel model)
        {
            PaperVersionDTO paperVersion = _paperVersionRepository.GetPaperVersion(model.Id);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (!User.IsInRole("Admin"))
            {
                StatusMessage = "Error. Access denied.";
                return RedirectToAction("MyPaper", "Papers", new { id = paperVersion.PaperId });
            }

            if (paperVersion == null)
            {
                StatusMessage = "Error. Version do not exists.";
                return RedirectToAction("MyPapers", "Papers");
            }
            var result =  _paperVersionRepository.SetStatusVersionRejected(model.Id);
            if (result == 1)
            {
                if (model.Comment != null)
                    _paperVersionRepository.SetComment(model.Id,model.Comment);
                StatusMessage = "Succesfully rejected.";
                return RedirectToAction("Index", "PaperVersions");
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction("MyPapers", "Papers");
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

        private List<SelectListItem> GetVersionsStatusesSelectListItem(string selected)
        {

            var result = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Status", Value = "" },
                new SelectListItem { Text = "Document recieved", Value = "0" },
                new SelectListItem { Text = "Waiting for reviews", Value = "1" },
                new SelectListItem { Text = "Accepted", Value = "2" },
                new SelectListItem { Text = "Rejected", Value = "3" },
                new SelectListItem { Text = "Minor revision", Value = "4" },
                new SelectListItem { Text = "Major revision", Value = "5" },
            };
            if (selected != null)
                result.FirstOrDefault(r => r.Value == selected.ToString()).Selected = true;

            return result;
        }

        private List<SelectListItem> GetVersionsSortBySelectListItem(int? selected)
        {

            var result = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Sort by", Value = "" },
                new SelectListItem { Text = "Title Asc", Value = "1" },
                new SelectListItem { Text = "Title Desc", Value = "2" },
                new SelectListItem { Text = "First name Asc", Value = "3" },
                new SelectListItem { Text = "First name Desc", Value = "4" },
                new SelectListItem { Text = "Last name Asc", Value = "5" },
                new SelectListItem { Text = "Last name Desc", Value = "6" },
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