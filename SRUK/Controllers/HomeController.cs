using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SRUK.Models;
using SRUK.Services.Interfaces;

namespace SRUK.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISeasonRepository _seasonRepository;

        public HomeController(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public IActionResult Index()
        {
            var currentSeason = _seasonRepository.GetCurrentSeason();
            if (currentSeason != null)
            {
                ViewBag.Edition = currentSeason.EditionNumber;
                ViewBag.Year = currentSeason.ConferenceStartDate.Year;
                ViewBag.Location = currentSeason.Location;
                ViewBag.ConferenceStartDate = currentSeason.ConferenceStartDate;
                ViewBag.ConferenceEndDate = currentSeason.ConferenceEndDate;
                
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
