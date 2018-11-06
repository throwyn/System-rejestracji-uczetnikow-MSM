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

        public PapersController(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            IPaperRepository paperRepository,
            ISeasonRepository seasonRepository
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _paperRepository = paperRepository;
            _seasonRepository = seasonRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Papers
        public async Task<ActionResult> Index()
        {

            if (User.IsInRole("Admin"))
            {
                var papers = _paperRepository.GetPapers();
                var model = new PaperIndexViewModel();
                model.Papers = papers.ToList();
                model.StatusMessage = StatusMessage;
                return View(model);
            }
                return RedirectToAction("Index", "Home");

        }

        // GET: Papers/Details/5
        [Route("Details/{id}")]
        public IActionResult Details(long? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                StatusMessage = "Error. Enter id of paper.";
                return RedirectToAction(nameof(Index));
            }

            var season = _paperRepository.GetPaperAsync((long)id).Result;
            if (season == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(Index));
            }
            var model = Mapper.Map<PaperDetailsViewModel>(season);
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // GET: Papers/Create
        [Route("Create")]
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var seasons = _seasonRepository.GetSeasons();
            ViewBag.Seasons = new List<SelectListItem>();
            foreach (var season in seasons)
            {
                ViewBag.Seasons.Add(new SelectListItem { Text = season.EndDate.Year.ToString(), Value = season.Id.ToString() });
            };


            var users = _userRepository.GetUsers();
            ViewBag.Users = new List<SelectListItem>();
            foreach(var user in users){
                ViewBag.Users.Add(new SelectListItem { Text = user.Email, Value = user.Id });
            };

            ViewBag.Statuses = new List<SelectListItem>
            {
                new SelectListItem { Text = "Created", Value = "0" },
                new SelectListItem { Text = "Topic accepted", Value = "1"  },
                new SelectListItem { Text = "Topic rejected", Value = "2"  }
            };

            var model = new PaperCreateViewModel();
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: Papers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PaperCreateViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (model == null)
            {
                StatusMessage = "Error. Something went wrong.";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                PaperDTO paper = Mapper.Map<PaperDTO>(model);
                var result = _paperRepository.AddPaperAsync(paper);
                if (result.Result == 1)
                {
                    StatusMessage = "Succesfully created.";
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Entered data is not valid.";
            return View(model);
        }

        // GET: Papers/Edit/5
        [Route("Edit/{id}")]
        public IActionResult Edit(long? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                StatusMessage = "Error. Enter id of paper.";
                return RedirectToAction(nameof(Index));
            }

            var paper = _paperRepository.GetPaperAsync((long)id).Result;

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
            return View(model);
        }

        // POST: Papers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(PaperEditViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    var paper = Mapper.Map<PaperDTO>(model);
                    var result = await _paperRepository.UpdatePaperAsync(paper);
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
        public IActionResult Delete(long? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                StatusMessage = "Error. Enter id of paper.";
                return RedirectToAction(nameof(Index));
            }

            var paper = _paperRepository.GetPaperAsync((long)id).Result;

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
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var result = await _paperRepository.DeletePaperAsync(id);
            if (result == 1)
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
        public async Task<IActionResult> ApproveTopic(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var result = await _paperRepository.ApproveTopic(id);
            if (result == 1)
            {
                StatusMessage = "Succesfully approved.";
                return RedirectToAction(nameof(Index));
            }
            else if (result == 2)
            {
                StatusMessage = "Error. You can approve only new topics!";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Papers/Edit/5/RejectTopic
        [HttpGet]
        [Route("RejectTopic/{id}")]
        public async Task<IActionResult> RejectTopic(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var result = await _paperRepository.RejectTopic(id);
            if (result == 1)
            {
                StatusMessage = "Succesfully rejected.";
                return RedirectToAction(nameof(Index));
            }
            else if (result == 2)
            {
                StatusMessage = "Error. You can approve only new topic!";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(Index));
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

            var model = new PaperCreateViewModel();
            model.AuthorId = user.Id;
            model.IsPaid = false;
            model.SeasonId = _seasonRepository.GetCurrentSeasonIdAsync().Result;
            model.Status = 0;

            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: Papers/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                var user = await _userManager.GetUserAsync(HttpContext.User);

                PaperDTO paper = Mapper.Map<PaperDTO>(model);
                paper.AuthorId = user.Id;
                paper.IsPaid = false;
                paper.SeasonId = _seasonRepository.GetCurrentSeasonIdAsync().Result;
                paper.Status = 0;

                var result = _paperRepository.AddPaperAsync(paper);
                if (result.Result == 1)
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
             if (User.IsInRole("Participant"))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var papers = _paperRepository.GetUserPapers(user.Id);
                var model = new PaperIndexViewModel();
                model.Papers = papers.ToList();
                model.StatusMessage = StatusMessage;
                ViewBag.IsRegistrationOpened = _seasonRepository.IsRegistrationOpenedAsync().Result;
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }

        // GET: Papers/EditMyPaper/5
        [Route("EditMyPaper/{id}")]
        public async Task<IActionResult> EditMyPaperAsync(long? id)
        {
            if (id == null)
            {
                StatusMessage = "Error. Enter id of paper.";
                return RedirectToAction(nameof(MyPapers));
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var paper = _paperRepository.GetPaperAsync((long)id).Result;
            if(paper.Author.Id != user.Id)
            {
                StatusMessage = "Error. You can edit only yours papers.";
                return RedirectToAction(nameof(MyPapers));
            }

            if (paper == null)
            {
                StatusMessage = "Error. Paper do not exists.";
                return RedirectToAction(nameof(MyPapers));
            }

            var model = Mapper.Map<PaperUserEditViewModel>(paper);

            return View(model);
        }

        // POST: Papers/EditMyPaper/5
        [HttpPost]
        [Route("EditMyPaper/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMyPaper(PaperUserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var paper = Mapper.Map<PaperDTO>(model);
                    var result = await _paperRepository.UpdatePaperTitleAsync(paper);
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







    }
}