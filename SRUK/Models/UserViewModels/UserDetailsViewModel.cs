using System;
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
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Display(Name = "Edit date")]
        public DateTime EditDate { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Academic title")]
        public string Degree { get; set; }

        [Display(Name = "VAT ID")]
        public string VATID { get; set; }

        [Display(Name = "Organisation address")]
        public string OrganisationAdderss { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        public string Address { get; set; }


        public IEnumerable<ParticipancyShortDTO> Participancies { get; set; }
        public IEnumerable<ReviewShortDTO> Reviews { get; set; }
        public IEnumerable<PaperShortDTO> Papers { get; set; }

        public string StatusMessage { get; set; }
    }
}
