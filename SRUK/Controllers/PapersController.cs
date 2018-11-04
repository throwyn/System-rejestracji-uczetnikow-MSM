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

        public PapersController(
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager,
            IPaperRepository paperRepository
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
            _paperRepository = paperRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Users
        public ActionResult Index()
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

        //// GET: Seasons/Details/5
        //[Route("Details/{id}")]
        //public IActionResult Details(long? id)
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    if (id == null)
        //    {
        //        StatusMessage = "Error. Enter id of season.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var season = _seasonRepository.GetSeason((long)id);
        //    if (season == null)
        //    {
        //        StatusMessage = "Error. Season do not exists.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(season);
        //}

        //// GET: Seasons/Create
        //[Route("Create")]
        //public IActionResult Create()
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    var model = new SeasonCreateViewModel();
        //    model.StatusMessage = StatusMessage;
        //    return View(model);
        //}

        //// POST: Seasons/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[Route("Create")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(SeasonCreateViewModel model)
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
        //        SeasonDTO season = Mapper.Map<SeasonDTO>(model);
        //        var result = _seasonRepository.AddSeasonAsync(season);
        //        if (result.Result == 1)
        //        {
        //            StatusMessage = "Succesfully created.";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    StatusMessage = "Error. Data that you entered is not valid.";
        //    return View(model);
        //}

        //// GET: Seasons/Edit/5
        //[Route("Edit/{id}")]
        //public IActionResult Edit(long? id)
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    if (id == null)
        //    {
        //        StatusMessage = "Error. Enter id of season.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var season = _seasonRepository.GetSeason((long)id);

        //    if (season == null)
        //    {
        //        StatusMessage = "Error. Season do not exists.";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    var model = Mapper.Map<SeasonEditViewModel>(season);
        //    return View(model);
        //}

        //// POST: Seasons/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[Route("Edit/{id}")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditAsync(SeasonEditViewModel model)
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var season = Mapper.Map<SeasonDTO>(model);
        //            var result = await _seasonRepository.UpdateSeasonAsync(season);
        //            if (result == 1)
        //            {
        //                StatusMessage = "Succesfully updated.";
        //                return RedirectToAction(nameof(Index));
        //            }
        //        }
        //        catch
        //        {
        //            StatusMessage = "Error. Something went wrong.";
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    StatusMessage = "Error. Something went wrong.";
        //    return RedirectToAction(nameof(Index));
        //}

        ////// GET: Seasons/Delete/5
        //[Route("Delete/{id}")]
        //public IActionResult Delete(long? id)
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    if (id == null)
        //    {
        //        StatusMessage = "Error. Enter id of season.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var season = _seasonRepository.GetSeason((long)id);

        //    if (season == null)
        //    {
        //        StatusMessage = "Error. Season do not exists.";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    var model = Mapper.Map<SeasonDeleteViewModel>(season);
        //    return View(model);
        //}

        ////// POST: Seasons/Delete/5
        //[HttpPost]
        //[Route("Delete/{id}")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    if (!User.IsInRole("Admin"))
        //        return RedirectToAction("Index", "Home");

        //    var result = await _seasonRepository.DeleteSeasonAsync(id);
        //    if (result == 1)
        //    {
        //        StatusMessage = "Succesfully deleted.";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    StatusMessage = "Error. Something went wrong.";
        //    return RedirectToAction(nameof(Index));
        //}

    }
}