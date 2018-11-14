using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<UserShortDTO> GetUsers()
        {
            var entityUser = _context.Users.Where(u => u.IsDeleted == false).OrderBy(u => u.Id).ToAsyncEnumerable().ToEnumerable();
            var users = Mapper.Map<IEnumerable<UserShortDTO>>(entityUser);
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

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
