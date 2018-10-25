using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SRUK.Data;
using SRUK.Models;
using SRUK.Services.Interfaces;

namespace SRUK.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserDTO GetUser(string Id)
        {
            var entityUser = _context.Users.SingleOrDefault(u => u.Id == Id);
            var user = Mapper.Map<UserDTO>(entityUser);
            return user;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var entityUser = _context.Users.OrderBy(u=>u.Id);
            var user = Mapper.Map<IEnumerable<UserDTO>>(entityUser);
            return user;
        }
    }
}
