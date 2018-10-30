﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class UserDetailsViewModel
    {
        [Display(Name ="ID")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Organisation")]
        public string Organisation { get; set; }
        

        [Display(Name = "Lockout end")]
        public DateTimeOffset LockoutEnd { get; set; }

        [Display(Name = "Email confirmed")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Phone confirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "Failed access count")]
        public int AccessFailedCount { get; set; }

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Edit date")]
        public DateTime EditDate { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        public string StatusMessage { get; set; }
    }
}
