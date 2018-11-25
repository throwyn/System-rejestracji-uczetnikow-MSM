using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Academic title")]
        public string Degree { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Name")]
        public string Organisation { get; set; }

        [Display(Name = "VAT Identification Number(NIP)")]
        public string VATID { get; set; }

        [Display(Name = "Organisation adderss")]
        public string OrganisationAdderss { get; set; }
        
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        
        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public string StatusMessage { get; set; }

    }
}
