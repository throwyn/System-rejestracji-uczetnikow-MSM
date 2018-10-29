using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SRUK.Data;
using SRUK.Models;
using SRUK.Services.Interfaces;

namespace SRUK.Services
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        //public UserDTO GetUser(string Id)
        //{
        //    var entityUser = _context.Users.SingleOrDefault(u => u.Id == Id);
        //    var user = Mapper.Map<UserDTO>(entityUser);
        //    return user;
        //}

        //public IEnumerable<UserIndexViewModel> GetUsers()
        //{
        //    var entityUser = _context.Users.OrderBy(u=>u.Id);
        //    var user = Mapper.Map<IEnumerable<UserIndexViewModel>>(entityUser);
        //    return user;
        //}

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
