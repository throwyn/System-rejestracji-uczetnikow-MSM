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
    //[Authorize]
    //[Route("[controller]/[action]")]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        //private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            IUserRepository userRepository,
            //IServiceProvider serviceProvider, 
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userRepository = userRepository;
            //_serviceProvider = serviceProvider;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _roleManager = roleManager;
        }


        // GET: Users
        public ActionResult Index()
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
            //var UserManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //var user = await UserManager.FindByEmailAsync("participant@prz.pl");
            //await UserManager.AddToRoleAsync(user, "Participant");


            var entityUsers = _userManager.Users.ToList();
            if (entityUsers == null) { return NotFound(); }

            var users = new List<UserIndexViewModel>();
            
            foreach(var entityUser in entityUsers)
            {
                var user = Mapper.Map<UserIndexViewModel>(entityUser);
                user.Role = _userManager.GetRolesAsync(entityUser).Result.FirstOrDefault();
                users.Add(user);
            }
            return View(users);
        }

        // GET: Users/Details/5
        [HttpGet]
        [Route("Details/{id}")]
        public ActionResult Details(string id)
        {
            var entityUser = _userManager.Users.SingleOrDefault(u=>u.Id == id);
            if(entityUser == null) { return NotFound(); }
            var user = Mapper.Map<UserDetailsViewModel>(entityUser);
            user.Role = _userManager.GetRolesAsync(entityUser).Result.FirstOrDefault();
            return View(user);
        }

        // GET: Users/Create
        [Route("Create")]
        public ActionResult Create()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View(new UserCreateViewModel());
        }

        // POST: Users/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UserCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var user = Mapper.Map<ApplicationUser>(model);
                user.UserName = model.Email;

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    _logger.LogInformation(User.Identity.Name+" created a new account.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: Users/Edit/5
        [Route("Edit")]
        public ActionResult Edit(string id)
        {
            ViewBag.Roles = _roleManager.Roles.ToList();
            var entityUser = _userManager.FindByIdAsync(id).Result;
            var user = Mapper.Map<UserEditViewModel>(entityUser);
            user.Role = _userManager.GetRolesAsync(entityUser).Result.FirstOrDefault();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(UserEditViewModel model)
        {
            //try
            //{
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = Mapper.Map<ApplicationUser>(model);
                user.UserName = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var roles = _userManager.GetRolesAsync(user).Result.ToAsyncEnumerable().ToEnumerable();
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    await _userManager.AddToRoleAsync(user, model.Role);
                    _logger.LogInformation(User.Identity.Name + " created a new account.");

                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return RedirectToAction(nameof(Edit));
            //}
        }

        // GET: Users/Delete/5
        [Route("Delete/{id}")]
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}