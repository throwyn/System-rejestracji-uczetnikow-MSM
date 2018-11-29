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

namespace SRUK.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class PapersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPaperRepository _paperRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IParticipanciesRepository _participanciesRepository;

        public PapersController(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            IPaperRepository paperRepository,
            ISeasonRepository seasonRepository,
            IParticipanciesRepository participanciesRepository
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _paperRepository = paperRepository;
            _seasonRepository = seasonRepository;
            _participanciesRepository = participanciesRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Papers
        public ActionResult Index()
        {

            if (User.IsInRole("Admin"))
            {
                var papers = _paperRepository.GetPapers();
                var model = new PaperIndexViewModel
                {
                    Papers = papers.ToList(),
                    StatusMessage = StatusMessage
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }

        // GET: Papers/Details/5
        [Route("Details/{id}")]
        public IActionResult Details(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var paper = _paperRepository.GetPaper(id);
            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(Index));
            }
            var model = Mapper.Map<PaperDetailsViewModel>(paper);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // GET: Papers/Create
        //[Route("Create")]
        //public IActionResult Create()
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    var seasons = _seasonRepository.GetSeasons();
        //    ViewBag.Seasons = new List<SelectListItem>();
        //    foreach (var season in seasons)
        //    {
        //        ViewBag.Seasons.Add(new SelectListItem { Text = season.Name, Value = season.Id.ToString() });
        //    };


        //    var users = _userRepository.GetUsers();
        //    ViewBag.Users = new List<SelectListItem>();
        //    foreach (var user in users) {
        //        ViewBag.Users.Add(new SelectListItem { Text = user.Email, Value = user.Id });
        //    };

        //    ViewBag.Statuses = new List<SelectListItem>
        //    {
        //        new SelectListItem { Text = "Created", Value = "0" },
        //        new SelectListItem { Text = "Topic accepted", Value = "1"  },
        //        new SelectListItem { Text = "Topic rejected", Value = "2"  }
        //    };

        //    var model = new PaperCreateViewModel();
        //    model.StatusMessage = StatusMessage;
        //    return View(model);
        //}

        // POST: Papers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[Route("Create")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(PaperCreateViewModel model)
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    if (model == null)
        //    {
        //        StatusMessage = "Error. Something went wrong.";
        //        return View(model);
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        if (_paperRepository.TitleTaken(model.Title).Result)
        //        {
        //            StatusMessage = "Error. This title is already taken.";
        //            return RedirectToAction(nameof(Create));
        //        }

        //        PaperDTO paper = Mapper.Map<PaperDTO>(model);
        //        var result = _paperRepository.AddPaperAsync(paper);
        //        if (result.Result == 1)
        //        {
        //            StatusMessage = "Succesfully created.";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    StatusMessage = "Error. Entered data is not valid.";
        //    return View(model);
        //}

        // GET: Papers/Edit/5
        [Route("Edit/{id}")]
        public IActionResult Edit(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");
            
            var paper = _paperRepository.GetPaper(id);

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Statuses = new List<SelectListItem>
            {
                new SelectListItem { Text = "Created", Value = "0" },
                new SelectListItem { Text = "Topic accepted", Value = "1"  },
                new SelectListItem { Text = "Topic rejected", Value = "2"  }
            };

            var model = Mapper.Map<PaperEditViewModel>(paper);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: Papers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult EditAsync(PaperEditViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    if (_paperRepository.TitleTakenExcept(model.Title,model.Id))
                    {
                        StatusMessage = "Error. This title is already taken.";
                        return RedirectToAction(nameof(Edit), model.Id);
                    }
                    var paper = Mapper.Map<PaperDTO>(model);
                    var result =  _paperRepository.UpdatePaper(paper);
                    if (result == 1)
                    {
                        StatusMessage = "Succesfully updated.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch
                {
                    StatusMessage = "Error. Something went wrong.";
                    return RedirectToAction(nameof(Index));
                }
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(Index));
        }

        //// GET: Papers/Delete/5
        [Route("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var paper = _paperRepository.GetPaper(id);

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(Index));
            }

            var model = Mapper.Map<PaperDeleteViewModel>(paper);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        //// POST: Papers/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var result = _paperRepository.DeletePaper(id);
            if (result > 0 )
            {
                StatusMessage = "Succesfully deleted.";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Papers/Edit/5/ApproveTopic
        [HttpGet]
        [Route("ApproveTopic/{id}")]
        public IActionResult ApproveTopic(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var result = _paperRepository.SetStatusTopicApproved(id);
            if (result == 1)
            {
                StatusMessage = "Succesfully approved.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                StatusMessage = "Error. You can approve only new topics!";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Papers/Edit/5/RejectTopic
        [HttpGet]
        [Route("RejectTopic/{id}")]
        public IActionResult RejectTopic(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var result = _paperRepository.SetStatusTopicRejected(id);
            if (result == 1)
            {
                StatusMessage = "Succesfully rejected.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                StatusMessage = "Error. You can reject only new topic!";
                return RedirectToAction(nameof(Index));
            }
        }


        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        //USERS ACTIONS


        // GET: Papers/Add
        [Route("Add")]
        public async Task<IActionResult> Add()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var participancy = _participanciesRepository.GetUserCurrentParticipancy(user.Id);
            if (participancy == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (participancy.User.Id != user.Id)
            {
                StatusMessage = "You cannot add topic for this person!";
                return RedirectToAction("Index", "Home");
            }

            var model = new PaperCreateViewModel
            {
                ParticipancyId = participancy.Id,
                AuthorId = user.Id,
                SeasonId = participancy.SeasonId,
                Status = 0,

                StatusMessage = StatusMessage
            };
            return View(model);
        }

        // POST: Papers/Add
        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PaperCreateViewModel model)
        {
            if (model == null)
            {
                StatusMessage = "Error. Something went wrong.";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                if (_paperRepository.TitleTaken(model.Title))
                {
                    StatusMessage = "Error. This title is already taken.";
                    return RedirectToAction(nameof(Add));
                }
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var participancy = _participanciesRepository.GetUserCurrentParticipancy(user.Id);

                PaperDTO paper = Mapper.Map<PaperDTO>(model);
                paper.ParticipancyId = model.ParticipancyId;
                paper.Status = 0;

                var result = _paperRepository.AddPaper(paper);
                if (result == 1)
                {
                    StatusMessage = "Succesfully created.";
                    return RedirectToAction(nameof(MyPapers));
                }
                return RedirectToAction(nameof(MyPapers));
            }
            StatusMessage = "Error. Entered data is not valid.";
            return View(model);
        }
        // GET: Papers
        [Route("MyPapers")]
        public async Task<ActionResult> MyPapers()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var papers = _paperRepository.GetUserPapers(user.Id);
            var model = new PaperIndexViewModel
            {
                Papers = papers.ToList(),
                StatusMessage = StatusMessage
            };
            ViewBag.IsRegistrationOpened = _seasonRepository.IsRegistrationOpened() && _participanciesRepository.UserWantsPublicationInThisSeason(user.Id);
            return View(model);

        }

        // GET: Papers/MyPaperEdit/5
        [Route("MyPaperEdit/{id}")]
        public async Task<IActionResult> MyPaperEdit(long id)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var paper = _paperRepository.GetPaper(id);
            var participancy = _participanciesRepository.GetParticipancy(paper.ParticipancyId);
            if (participancy.User.Id != user.Id)
            {
                StatusMessage = "You cannot edit this topic!";
                return RedirectToAction("Index", "Home");
            }

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(MyPapers));
            }

            var model = Mapper.Map<MyPaperEditViewModel>(paper);

            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: Papers/MyPaperEdit/5
        [HttpPost]
        [Route("MyPaperEdit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyPaperEdit(MyPaperEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_paperRepository.TitleTaken(model.Title))
                    {
                        StatusMessage = "Error. This title is already taken.";
                        return RedirectToAction(nameof(MyPaperEdit), model.Id);
                    }

                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    var paper = _paperRepository.GetPaper(model.Id);
                    var participancy = _participanciesRepository.GetParticipancy(paper.ParticipancyId);
                    if (participancy.User.Id != user.Id)
                    {
                        StatusMessage = "You cannot edit this topic!";
                        return RedirectToAction("Index", "Home");
                    }

                    var newPaper = Mapper.Map<PaperDTO>(model);
                    var result =  _paperRepository.UpdatePaperTitle(newPaper);
                    if (result == 1)
                    {
                        StatusMessage = "Succesfully updated.";
                        return RedirectToAction(nameof(MyPapers));
                    }
                }
                catch
                {
                    StatusMessage = "Error. Something went wrong.";
                    return RedirectToAction(nameof(MyPapers));
                }
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(MyPapers));
        }

        // GET: Papers
        [Route("MyPaper/{id}")]
        public async Task<ActionResult> MyPaper(long id)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var paper = _paperRepository.GetPaper(id);

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(MyPapers));
            }
            var participancy = _participanciesRepository.GetParticipancy(paper.ParticipancyId);
            if (participancy.User.Id != user.Id)
            {
                StatusMessage = "Error. Access denied!";
                return RedirectToAction(nameof(MyPapers));
            }
            var model = Mapper.Map<MyPaperDetailsViewModel>(paper);
            model.StatusMessage = StatusMessage;
            ViewBag.IsRegistrationOpened = _seasonRepository.IsRegistrationOpened();
            return View(model);

        }

        // GET: Papers/MyPaperDelete/5
        [HttpGet]
        [Route("MyPaperDelete/{id}")]
        public async Task<IActionResult> MyPaperDelete(long id)
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var paper = _paperRepository.GetPaper(id);

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(MyPapers));
            }
            var participancy = _participanciesRepository.GetParticipancy(paper.ParticipancyId);
            if (participancy.User.Id != user.Id)
            {
                StatusMessage = "Error. Access denied!";
                return RedirectToAction(nameof(MyPapers));
            }

            var model = Mapper.Map<MyPaperDetailsViewModel>(paper);

            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: Papers/MyPaperDelete/5
        [HttpPost]
        [Route("MyPaperDelete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyPaperDelete(MyPaperDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    var paper = _paperRepository.GetPaper(model.Id);

                    if (paper == null)
                    {
                        StatusMessage = "Error. Paper do not exists.";
                        return RedirectToAction(nameof(MyPapers));
                    }
                    var participancy = _participanciesRepository.GetParticipancy(paper.ParticipancyId);
                    if (participancy.User.Id != user.Id)
                    {
                        StatusMessage = "Error. Access denied!";
                        return RedirectToAction(nameof(MyPapers));
                    }

                    var result = _paperRepository.DeletePaper(model.Id);
                    if (result > 0)
                    {
                        StatusMessage = "Succesfully deleted.";
                        return RedirectToAction(nameof(MyPapers));
                    }
                }
                catch
                {
                    StatusMessage = "Error. Something went wrong.";
                    return RedirectToAction(nameof(MyPapers));
                }
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(MyPapers));
        }





    }
}