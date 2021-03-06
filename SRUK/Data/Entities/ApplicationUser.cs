﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SRUK.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string Degree { get; set; }

        [MaxLength(100)]
        public string Organisation { get; set; }

        public string VATID { get; set; }

        public string OrganisationAdderss { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime EditDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


        public virtual ICollection<Participancy> Participancies { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
