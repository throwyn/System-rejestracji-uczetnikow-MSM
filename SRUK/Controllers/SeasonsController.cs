using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SRUK.Data;
using SRUK.Entities;
using SRUK.Models;
using SRUK.Services.Interfaces;

namespace SRUK.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SeasonsController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonsController(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Seasons
        public IActionResult Index()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var seasons = _seasonRepository.GetSeasons();
            var model = new SeasonIndexViewModel();
            model.Seasons = seasons.ToList();
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // GET: Seasons/Details/5
        [Route("Details/{id}")]
        public IActionResult Details(long? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                StatusMessage = "Error. Enter id of season.";
                return RedirectToAction(nameof(Index));
            }

            var season = _seasonRepository.GetSeason((long)id);
            if (season == null)
            {
                StatusMessage = "Error. Season do not exists.";
                return RedirectToAction(nameof(Index));
            }

            return View(season);
        }

        // GET: Seasons/Create
        public IActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var model = new SeasonCreateViewModel();
            model.StatusMessage = StatusMessage;
            return View(model);
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SeasonCreateViewModel model)
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
                SeasonDTO season = Mapper.Map<SeasonDTO>(model);
                var result = _seasonRepository.AddSeasonAsync(season);
                if (result.Result == 1)
                {
                    StatusMessage = "Succesfully created.";
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Data that you entered is not valid.";
            return View(model);
        }

        // GET: Seasons/Edit/5
        [Route("Edit/{id}")]
        public IActionResult Edit(long? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                StatusMessage = "Error. Enter id of season.";
                return RedirectToAction(nameof(Index));
            }

            var season = _seasonRepository.GetSeason((long)id);

            if (season == null)
            {
                StatusMessage = "Error. Season do not exists.";
                return RedirectToAction(nameof(Index));
            }
            var model = Mapper.Map<SeasonEditViewModel>(season);
            return View(model);
        }

        // POST: Seasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(SeasonEditViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    var season = Mapper.Map<SeasonDTO>(model);
                    var result = await _seasonRepository.UpdateSeasonAsync(season);
                    if(result == 1)
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

        //// GET: Seasons/Delete/5
        [Route("Delete/{id}")]
        public IActionResult Delete(long? id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                StatusMessage = "Error. Enter id of season.";
                return RedirectToAction(nameof(Index));
            }

            var season = _seasonRepository.GetSeason((long)id);

            if (season == null)
            {
                StatusMessage = "Error. Season do not exists.";
                return RedirectToAction(nameof(Index));
            }

            var model = Mapper.Map<SeasonDeleteViewModel>(season);
            return View(model);
        }

        //// POST: Seasons/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]                                                                                                                                                                                                                                                                                                                               
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var result = await _seasonRepository.DeleteSeasonAsync(id);
            if (result == 1)
            {
                StatusMessage = "Succesfully deleted.";
                return RedirectToAction(nameof(Index));
            }
            StatusMessage = "Error. Something went wrong.";
            return RedirectToAction(nameof(Index));
        }

        //private bool SeasonExists(long id)
        //{
        //    return _context.Season.Any(e => e.Id == id);
        //}
    }
}
