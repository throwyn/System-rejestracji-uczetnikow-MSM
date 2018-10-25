﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRUK.Models;

namespace SRUK.Services.Interfaces
{
    interface IUserRepository
    {
        IEnumerable<UserDTO> GetUsers();

        UserDTO GetUser(string Id);

    }
}