﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Degree { get; set; }
        public string VATID { get; set; }
        public string Organisation { get; set; }
        public string PhoneNumber { get; set; }

        public bool LockoutEnabled { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }

        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
