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
    public class ParticipanciesController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPaperRepository _paperRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly IParticipanciesRepository _participanciesRepository;

        public ParticipanciesController(
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

        // GET: Participancies
        public ActionResult Index()
        {

            if (User.IsInRole("Admin"))
            {
                var participancies = _participanciesRepository.GetParticipancies();
                var model = new ParticipancyIndexViewModel()
                {
                    Participancies = participancies.ToList(),
                    StatusMessage = StatusMessage
                };
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }
        // GET: Participancies/SignUp
        [HttpGet]
        [Route("SignUp")]
        public ActionResult SignUp()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var currentSeason = _seasonRepository.GetCurrentSeason();
            if (currentSeason == null)
            {
                StatusMessage = "Error. Signing up is forbidden for now.";
                return RedirectToAction("MyParticipancies");
            }
            if (_participanciesRepository.GetUserCurrentParticipancy(userId) != null)
            {
                StatusMessage = "Error. You are already signed up!.";
                return RedirectToAction("MyParticipancies");
            }
            var model = new ParticipancySignUpViewModel()
            {
                SeasonName = currentSeason.Name + " " + currentSeason.EditionNumber,
                StatusMessage = StatusMessage,
                Deadline = currentSeason.EndDate
            };
            return View(model);
        }

        // POST: Participancies/SignUp
        [HttpPost]
        [Route("SignUp")]
        public ActionResult SignUp(ParticipancySignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var currentSeason = _seasonRepository.GetCurrentSeason();
                if (currentSeason == null)
                {
                    StatusMessage = "Error. Signing up is forbidden for now.";
                    return RedirectToAction("MyParticipancies");
                }
                if (_participanciesRepository.GetUserCurrentParticipancy(userId) != null)
                {
                    StatusMessage = "Error. You are already signed up!.";
                    return RedirectToAction("MyParticipancies");
                }
                var participancy = Mapper.Map<ParticipancyDTO>(model);
                participancy.UserId = userId;
                participancy.SeasonId = currentSeason.Id;
                var result = _participanciesRepository.AddParticipancy(participancy);
                if (result == 1)
                {
                    StatusMessage = "Succesfully signed.";
                    return RedirectToAction(nameof(MyParticipancies));
                }

            }
            model.StatusMessage = "Error. Entered data is not valid.";
            return View(model);
        }
        // GET: Participancies/Edit/5
        [HttpGet]
        [Route("Edit/{id}")]
        public ActionResult Edit(long id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var participation = _participanciesRepository.GetParticipancy(id);
            if(participation.UserId != userId)
            {
                StatusMessage = "Error, you don't have permission to do that.";
                return RedirectToAction("MyParticipancies");
            }
            var season = _seasonRepository.GetSeason(participation.SeasonId);
            var currentSeason = _seasonRepository.GetCurrentSeason();
            if (season == null || season.Id != currentSeason.Id)
            {
                StatusMessage = "Error. Editing is forbidden for now.";
                return RedirectToAction("MyParticipancies");
            }
            var model = Mapper.Map<ParticipancyEditViewModel>(participation);
            model.SeasonName = currentSeason.Name + " " + currentSeason.EditionNumber;
            return View(model);
        }

        // POST: Participancies/Edit
        [HttpPost]
        [Route("Edit/{id}")]
        public ActionResult Edit(ParticipancyEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var existingParticipation = _participanciesRepository.GetParticipancy(model.Id);
                if(existingParticipation == null)
                {
                    StatusMessage = "Error, you don't have permission to do that.";
                    return RedirectToAction("MyParticipancies");
                }
                if (existingParticipation.UserId != userId)
                {
                    StatusMessage = "Error, you don't have permission to do that.";
                    return RedirectToAction("MyParticipancies");
                }
                var participancy = Mapper.Map<ParticipancyDTO>(model);
                var result = _participanciesRepository.UpdateParticipancy(participancy);
                if (result == 1)
                {
                    StatusMessage = "Succesfully edited.";
                    return RedirectToAction(nameof(MyParticipancies));
                }

            }
            model.StatusMessage = "Error. Entered data is not valid.";
            return View(model);
        }

        // GET: Participancies/MyParticipancies
        [Route("MyParticipancies")]
        public ActionResult MyParticipancies()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var userParticipancies = _participanciesRepository.GetUserParticipancies(userId);
            var currentSeason = _seasonRepository.GetCurrentSeason();
            var model = new MyParticipanciesViewModel()
            {
                Participancies = userParticipancies.ToList(),
                StatusMessage = StatusMessage
            };
            if (currentSeason != null)
            {
                ViewBag.CurrentSeason = currentSeason.Name + " " + currentSeason.EditionNumber;
                ViewBag.SignedForCurrentSeason = _participanciesRepository.GetUserCurrentParticipancy(userId)!=null?true:false;
            }
            return View(model);
        }


    }
}