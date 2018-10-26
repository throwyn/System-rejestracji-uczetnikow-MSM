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

namespace SRUK.Controllers
{
    //[Authorize]
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        public UsersController(IUserRepository userRepository,IServiceProvider serviceProvider)
        {
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
        }


        // GET: Users
        public async Task<ActionResult> IndexAsync()
        {
            //initializing custom roles 
            //var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //string[] roleNames = { "Admin", "Critic", "Participant" };
            //IdentityResult roleResult;

            //foreach (var roleName in roleNames)
            //{
            //    var roleExist = await RoleManager.RoleExistsAsync(roleName);
            //    if (!roleExist)
            //    {
            //        //create the roles and seed them to the database: Question 1
            //        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
            //    }
            //}
            var UserManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await UserManager.FindByEmailAsync("participant@prz.pl");
            await UserManager.AddToRoleAsync(user, "Participant");


            var users = _userRepository.GetUsers();
            return View(users);
        }

        // GET: Users/Details/5
        [HttpGet]
        public ActionResult Details(string id)
        {
            var user = _userRepository.GetUser(id);
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}