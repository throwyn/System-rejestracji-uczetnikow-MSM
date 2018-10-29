using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class UserIndexViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organisation { get; set; }

        public bool LockoutEnabled { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }

        public string Role { get; set; }
    }
}
