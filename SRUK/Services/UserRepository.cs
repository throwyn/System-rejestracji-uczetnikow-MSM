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

        public IEnumerable<UserShortDTO> GetUsers()
        {
            var entityUser = _context.Users.Where(u => u.IsDeleted == false).OrderBy(u => u.Id).ToAsyncEnumerable().ToEnumerable();
            var users = Mapper.Map<IEnumerable<UserShortDTO>>(entityUser);
            return users;
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
