using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityFrameworkPaginate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRUK.Data;
using SRUK.Entities;
using SRUK.Models;
using SRUK.Services.Interfaces;
using SRUK.Extensions;

namespace SRUK.Services
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        //public UserDTO GetUser(string Id)
        //{
        //    var entityUser = _context.Users.SingleOrDefault(u => u.Id == Id);
        //    var user = Mapper.Map<UserDTO>(entityUser);
        //    return user;
        //}

        public List<UserShortDTO> GetUsers()
        {
            var entityUsers = _context.Users.Where(u => u.IsDeleted != true).OrderBy(u => u.CreationDate).ToAsyncEnumerable().ToEnumerable();
            List<UserShortDTO> users = new List<UserShortDTO>();
            
            foreach (var entityUser in entityUsers)
            {
                var user = Mapper.Map<UserShortDTO>(entityUser);
                user.Role = _userManager.GetRolesAsync(entityUser).Result.FirstOrDefault();
                if (user.LockoutEnd < DateTime.Now)
                    user.LockoutEnd = DateTimeOffset.MinValue;
                users.Add(user);
            }
            
            return users;
        }
        public UserDTO GetUser(string id)
        {
            var entityUser = _context.Users.FirstOrDefault(u => u.Id == id);
            var user = Mapper.Map<UserDTO>(entityUser);
            return user;
        }
        public ApplicationUser GetApplicationUser(string id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            //var user = Mapper.Map<UserDTO>(entityUser);
            return user;
        }

        public IEnumerable<UserShortDTO> GetAdminsAndCritics()
        {
            var critics = _userManager.GetUsersInRoleAsync("Critic").Result;
            var admins = _userManager.GetUsersInRoleAsync("Admin").Result;
            var entityUsers = critics.Concat(admins);

            var users = Mapper.Map<IEnumerable<UserShortDTO>>(entityUsers);
            return users;
        }

        public Page<UserShortDTO> GetFilteredUsers(
            short sortBy,
            string degree,
            string firstName,
            string lastName,
            string organisation,
            string email,
            string role,
            int pageSize,
            int currentPage
        )
        {
            if (pageSize == 0) pageSize = 10;
            if (currentPage == 0) currentPage = 1;

            IEnumerable<UserShortDTO> results = GetUsers();

            results = !string.IsNullOrEmpty(degree) ? results.Where(u => u.Degree != null && u.Degree == degree) : results;
            results = !string.IsNullOrEmpty(firstName) ? results.Where(u => u.FirstName != null && u.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(lastName) ? results.Where(u => u.LastName != null && u.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(organisation) ? results.Where(u => u.Organisation != null && u.Organisation.Contains(organisation, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(email) ? results.Where(u => u.Email != null && u.Email.Contains(email, StringComparison.OrdinalIgnoreCase)) : results;
            results = !string.IsNullOrEmpty(role) ? results.Where(u => u.Role != null && u.Role == role) : results;

            results = sortBy == 1 ? results.OrderBy(u => u.Degree) : results;
            results = sortBy == 2 ? results.OrderByDescending(u => u.Degree) : results;
            results = sortBy == 3 ? results.OrderBy(u => u.FirstName) : results;
            results = sortBy == 4 ? results.OrderByDescending(u => u.FirstName) : results;
            results = sortBy == 5 ? results.OrderBy(u => u.LastName) : results;
            results = sortBy == 6 ? results.OrderByDescending(u => u.LastName) : results;
            results = sortBy == 7 ? results.OrderBy(u => u.Organisation) : results;
            results = sortBy == 8 ? results.OrderByDescending(u => u.Organisation) : results;
            results = sortBy == 9 ? results.OrderBy(u => u.Email) : results;
            results = sortBy == 10 ? results.OrderByDescending(u => u.Email) : results;
            results = sortBy == 11 ? results.OrderBy(u => u.Role) : results;
            results = sortBy == 12 || sortBy == 0 ? results.OrderByDescending(u => u.Role) : results;

            int recordCount = results.Count();
            int pageCount = (int)Math.Ceiling((decimal)recordCount / (decimal)pageSize);
            results = results.Skip((currentPage - 1) * pageSize).Take(pageSize);


            Page<UserShortDTO> users = new Page<UserShortDTO>()
            {
                Results = results,
                RecordCount = recordCount,
                PageCount = pageCount,
                CurrentPage = currentPage,
                PageSize = pageSize

            };

            return users;
        }



        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
