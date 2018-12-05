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
            string sortOrder = "", 
            string degree = "", 
            string firstName = "", 
            string lastName = "", 
            string organisation = "", 
            string email = "",
            string role = "")
        {
            if (!User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home");

            var users = _userRepository.GetUsers();

            //Filters
            ViewData["DegreeFilter"] = degree;
            ViewData["FirstNameFilter"] = firstName;
            ViewData["LastNameFilter"] = lastName;
            ViewData["OrganisationFilter"] = organisation;
            ViewData["EmailFilter"] = email;
            ViewData["RoleFilter"] = role;

            //Sort Params
            ViewData["DegreeSortParm"] = sortOrder == "Degree" ? "degree_desc" : "Degree";
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "LastName" ? "lastname_desc" : "LastName";
            ViewData["OrganisationSortParm"] = sortOrder == "Organisation" ? "organisation_desc" : "Organisation";
            ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";
            ViewData["RoleSortParm"] = sortOrder == "Role" ? "role_desc" : "Role";
            ViewData["BlockedSortParm"] = sortOrder == "Blocked" ? "blocked_desc" : "Blocked";

            IEnumerable<UserShortDTO> sortedUsers;

            switch (sortOrder)
            {
                case "degree_desc":
                    sortedUsers = users.OrderByDescending(e => e.Degree);
                    break;
                case "firstname_desc":
                    sortedUsers = users.OrderByDescending(e => e.FirstName);
                    break;
                case "lastname_desc":
                    sortedUsers = users.OrderByDescending(e => e.LastName);
                    break;
                case "organisation_desc":
                    sortedUsers = users.OrderByDescending(e => e.Organisation);
                    break;
                case "email_desc":
                    sortedUsers = users.OrderByDescending(e => e.Email);
                    break;
                case "role_desc":
                    sortedUsers = users.OrderByDescending(e => e.Role);
                    break;
                case "blocked_desc":
                    sortedUsers = users.OrderByDescending(e => e.LockoutEnd);
                    break;
                case "Degree":
                    sortedUsers = users.OrderBy(e => e.Degree);
                    break;
                case "LastName":
                    sortedUsers = users.OrderBy(e => e.LastName);
                    break;
                case "Organisation":
                    sortedUsers = users.OrderBy(e => e.Organisation);
                    break;
                case "Email":
                    sortedUsers = users.OrderBy(e => e.Email);
                    break;
                case "Role":
                    sortedUsers = users.OrderBy(e => e.Role);
                    break;
                case "Blocked":
                    sortedUsers = users.OrderBy(e => e.LockoutEnd);
                    break;
                default:
                    sortedUsers = users.OrderBy(e => e.FirstName);
                    break;
            }
            if (degree != null)
                sortedUsers = sortedUsers.Where(u => u.Degree != null && u.Degree.Contains(degree));
            if (email != null)
                sortedUsers = sortedUsers.Where(u => u.Email != null && u.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
            if (firstName != null)
                sortedUsers = sortedUsers.Where(u => u.FirstName != null && u.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase));
            if (lastName != null)
                sortedUsers = sortedUsers.Where(u => u.LastName != null && u.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
            if (organisation != null)
                sortedUsers = sortedUsers.Where(u => u.Organisation != null && u.Organisation.Contains(organisation, StringComparison.OrdinalIgnoreCase));
            if (role != null)
                sortedUsers = sortedUsers.Where(u => u.Role != null && u.Role.Contains(role, StringComparison.OrdinalIgnoreCase));

            //int entityUserNumber = entityUsers.Count;
            //if(entityUserNumber < ((pageNumber-1) * pageSize))
            //{
            //    StatusMessage = "Error. Out of range.";
            //    return RedirectToAction(nameof(Index));
            //}

            ViewBag.Degrees =   new AcademicDegrees().SelectListItems;
            ViewBag.Roles = GetRolesSelectListItem();

            var model = new UserIndexViewModel();
            model.User = sortedUsers.ToList();
            model.StatusMessage = StatusMessage;
            model.SortOrder = sortOrder;

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

            ViewBag.Degrees = new AcademicDegrees().SelectListItems;
            var model = new UserCreateViewModel();
            model.StatusMessage = StatusMessage;
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


            ViewBag.Degrees = new AcademicDegrees().SelectListItems;

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

                //var user = Mapper.Map<ApplicationUser>(model);
                user.UserName = model.Email;
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
                //await _userManager.RemovePasswordAsync(user);
                //user = _userManager.FindByIdAsync(id).Result;
                //var user = Mapper.Map<ApplicationUser>(model);
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
    
            #endregion
    }
}