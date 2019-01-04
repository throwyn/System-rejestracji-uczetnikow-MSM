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
using SRUK.Helpers;
using SRUK.Entities;
using SRUK.Models.AccountViewModels;
using SRUK.Services;
using SRUK.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using SRUK.Extensions;

namespace SRUK.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        //private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IPaperRepository _paperRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            IUserRepository userRepository,
            //IServiceProvider serviceProvider, 
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IPaperRepository paperRepository,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userRepository = userRepository;
            //_serviceProvider = serviceProvider;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _paperRepository = paperRepository;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        // GET: Users
        public ActionResult Index(
            int currentPage = 1,
            short sortBy = 0,
            string degree = "",
            string firstName = "",
            string lastName = "",
            string organisation = "",
            string email = "",
            string role = "")
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var filteredList = _userRepository.GetFilteredUsers(
             sortBy, degree, firstName, lastName, organisation, email, role, 5, currentPage);

            ViewBag.Degrees =   new AcademicDegrees().SelectListItems(degree);
            ViewBag.Roles = GetRolesSelectListItem(role);
            ViewBag.SortBy = GetUsersSortBySelectListItem();

            var model = new UserIndexViewModel
            {
                Degree = degree,
                FirstName = firstName,
                LastName = lastName,
                Organisation = organisation,
                Email = email,


                Results = filteredList.Results,
                CurrentPage = filteredList.CurrentPage,
                PageCount = filteredList.PageCount,
                PageSize = filteredList.PageSize,
                RecordCount = filteredList.RecordCount,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        // GET: Users/Details/5
        [HttpGet]
        [Route("Details/{id}")]
        public ActionResult Details(string id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var entityUser = _userManager.FindByIdAsync(id).Result;
            if (entityUser == null)
            {
                StatusMessage = "Error. User do not exists.";
                return RedirectToAction(nameof(Index));
            }
            var user = Mapper.Map<UserDetailsViewModel>(entityUser);
            user.Papers = _paperRepository.GetUserPapers(user.Id);
            user.Role = _userManager.GetRolesAsync(entityUser).Result.FirstOrDefault();
            user.StatusMessage = StatusMessage;
            return View(user);
        }

        // GET: Users/Create
        [Route("Create")]
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            ViewBag.Roles = _roleManager.Roles.ToList();

            ViewBag.Degrees = new AcademicDegrees().SelectListItems();
            var model = new UserCreateViewModel
            {
                StatusMessage = StatusMessage
            };
            return View(model);
        }

        // POST: Users/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UserCreateViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

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
                    _logger.LogInformation(User.Identity.Name + " created a new account.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    StatusMessage = "Success. User created.";
                    return RedirectToAction(nameof(Index));
                }
                else if (result.Errors.FirstOrDefault().Code == "DuplicateUserName")
                {
                    StatusMessage = "Error. Email is already taken.";
                    return RedirectToAction(nameof(Create));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                model.StatusMessage = StatusMessage;
                return RedirectToAction(nameof(Create));
            }
        }

        // GET: Users/Edit/5
        [Route("Edit/{id}")]
        public ActionResult Edit(string id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            ViewBag.Roles = _roleManager.Roles.ToList();
            var entityUser = _userManager.FindByIdAsync(id).Result;
            var user = Mapper.Map<UserEditViewModel>(entityUser);
            if (entityUser == null)
            {
                StatusMessage = "Error. User do not exists.";
                return RedirectToAction(nameof(Index));
            }
            user.Role = _userManager.GetRolesAsync(entityUser).Result.FirstOrDefault();


            ViewBag.Degrees = new AcademicDegrees().SelectListItems();

            user.StatusMessage = StatusMessage;
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(UserEditViewModel model)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

                if (model.Password != null)
                {
                    await _userManager.RemovePasswordAsync(user);
                    var setPasstordResult = await _userManager.AddPasswordAsync(user, model.Password);
                    if (!setPasstordResult.Succeeded)
                    {
                        StatusMessage = "Error. Password update went wrong.";
                        model.StatusMessage = StatusMessage;
                        return View(model);
                    }
                }

                user.UserName = model.Email;
                user.Country = model.Country;
                user.City = model.City;
                user.PostalCode = model.PostalCode;
                user.Address = model.Address;
                user.OrganisationAdderss = model.OrganisationAdderss;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Organisation = model.Organisation;
                user.Degree = model.Degree;
                user.VATID = model.VATID;
                user.PhoneNumber = model.PhoneNumber;
                user.EmailConfirmed = model.EmailConfirmed;
                user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
                user.SecurityStamp = Guid.NewGuid().ToString();

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var roles = _userManager.GetRolesAsync(user).Result.ToAsyncEnumerable().ToEnumerable();
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    await _userManager.AddToRoleAsync(user, model.Role);
                    _logger.LogInformation(User.Identity.Name + " created a new account.");

                    return RedirectToAction(nameof(Index));
                }
                StatusMessage = "Error. Update went wrong.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                StatusMessage = "Error. Something went wrong.";
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: Users/Delete/5
        [Route("Delete/{id}")]
        public ActionResult Delete(string id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var entityUser = _userManager.FindByIdAsync(id).Result;
            if (entityUser == null) { return NotFound(); }
            var user = Mapper.Map<UserDetailsViewModel>(entityUser);
            user.Role = _userManager.GetRolesAsync(entityUser).Result.FirstOrDefault();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id, IFormCollection collection)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            try
            {
                var user = _userManager.FindByIdAsync(id).Result;
                user.UserName = user.Id;
                user.NormalizedUserName = null;
                user.NormalizedEmail = null;
                user.AccessFailedCount = 0;
                user.Email = "deleted";
                user.FirstName = null;
                user.LastName = null;
                user.Organisation = null;
                user.Degree = null;
                user.VATID = null;
                user.PhoneNumber = null;
                user.EmailConfirmed = false;
                user.PhoneNumberConfirmed = false;
                var roles = _userManager.GetRolesAsync(user).Result.ToAsyncEnumerable().ToEnumerable();
                await _userManager.RemoveFromRolesAsync(user, roles);

                var result = await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #region Helpers

        private List<SelectListItem> GetRolesSelectListItem()
        {

            var roles = _roleManager.Roles.ToList();

            var result = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Role", Value = "" }
            };

            foreach (var role in roles)
            {
                result.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            }

            return result;
        }
        private List<SelectListItem> GetRolesSelectListItem(string selectedValue)
        {

            var roles = _roleManager.Roles.ToList();

            var result = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Role", Value = "" }
            };

            foreach (var role in roles)
            {
                result.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            }

            if (selectedValue != null && result.Where(r => r.Value == selectedValue).Count() > 0)
                result.FirstOrDefault(r => r.Value == selectedValue).Selected = true;

            return result;
        }

        private List<SelectListItem> GetUsersSortBySelectListItem()
        {

            return new List<SelectListItem>()
            {
                new SelectListItem { Text = "Sort by", Value = "" },
                new SelectListItem { Text = "Degree Asc", Value = "1" },
                new SelectListItem { Text = "Degree Desc", Value = "2" },
                new SelectListItem { Text = "First name Asc", Value = "3" },
                new SelectListItem { Text = "First name Desc", Value = "4" },
                new SelectListItem { Text = "Last name Asc", Value = "5" },
                new SelectListItem { Text = "Last name Desc", Value = "6" },
                new SelectListItem { Text = "Organisation Asc", Value = "7" },
                new SelectListItem { Text = "Organisation Desc", Value = "8" },
                new SelectListItem { Text = "Email Asc", Value = "9" },
                new SelectListItem { Text = "Email Desc", Value = "10" },
                new SelectListItem { Text = "Role Asc", Value = "11" },
                new SelectListItem { Text = "Role Desc", Value = "12" },
            };
        }

        #endregion
    }
}