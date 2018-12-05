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
            var entityUsers = _context.Users.Where(u => u.IsDeleted != true).OrderBy(u => u.Id).ToAsyncEnumerable().ToEnumerable();
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
            var entityUser = _context.Users.Where(u => u.Id == id);
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
            int pageSize, 
            int currentPage,
            short sortBy,
            string degree,
            string firstName,
            string lastName,
            string organisation,
            string email,
            string role
        )
        {
            //Page<UserShortDTO> users;

            var filters = new Filters<ApplicationUser>();
            var sorts = new Sorts<ApplicationUser>();

            filters.Add(!string.IsNullOrEmpty(degree), x => x.Degree.Contains(degree));
            filters.Add(!string.IsNullOrEmpty(firstName), x => x.FirstName.Equals(firstName));
            filters.Add(!string.IsNullOrEmpty(lastName), x => x.LastName.Equals(lastName));
            filters.Add(!string.IsNullOrEmpty(organisation), x => x.Organisation.Equals(organisation));
            filters.Add(!string.IsNullOrEmpty(email), x => x.Email.Equals(email));
            //filters.Add(!string.IsNullOrEmpty(role), x => x.Role.Equals(firstName));

            sorts.Add(sortBy == 1, x => x.Degree);
            sorts.Add(sortBy == 2, x => x.FirstName);
            sorts.Add(sortBy == 3, x => x.LastName);
            sorts.Add(sortBy == 3, x => x.Organisation);
            sorts.Add(sortBy == 3, x => x.Email);
           // sorts.Add(sortBy == 3, x => x.Role);

            var entityUsers = _context.Users.Paginate(currentPage, pageSize, sorts, filters);

            var users = Mapper.Map<Page<UserShortDTO>>(entityUsers);

            return users;
        }



        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
