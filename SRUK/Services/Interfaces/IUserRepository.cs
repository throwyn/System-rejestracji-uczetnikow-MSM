using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkPaginate;
using SRUK.Entities;
using SRUK.Models;

namespace SRUK.Services.Interfaces
{
    public interface IUserRepository
    {
        List<UserShortDTO> GetUsers();
        IEnumerable<UserShortDTO> GetAdminsAndCritics();
        UserDTO GetUser(string id);
        ApplicationUser GetApplicationUser(string id);
        Page<UserShortDTO> GetFilteredUsers(int pageSize,int currentPage,short sortBy,string degree,string firstName,string lastName,string organisation,string email,string role);

        //UserDTO GetUser(string Id);

    }
}
