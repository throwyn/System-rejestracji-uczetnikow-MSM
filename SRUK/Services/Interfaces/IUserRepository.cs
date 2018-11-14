using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRUK.Entities;
using SRUK.Models;

namespace SRUK.Services.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserShortDTO> GetUsers();
        IEnumerable<UserShortDTO> GetAdminsAndCritics();
        UserDTO GetUser(string id);
        ApplicationUser GetApplicationUser(string id);

        //UserDTO GetUser(string Id);

    }
}
