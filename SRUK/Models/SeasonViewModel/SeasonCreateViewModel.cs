using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class SeasonCreateViewModel
    {
        [Required]
        [Display(Name = "Main image filename")]
        [StringLength(100,MinimumLength = 5)]
        public string MainImageFileName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start date of conference")]
        public DateTime ConferenceStartDate { get; set; }

        [Required]
        [Display(Name = "End date of conference")]
        public DateTime ConferenceEndDate { get; set; }

        [Required]
        [Display(Name = "Locations")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Number of edition(Roman numerals recommended)")]
        [StringLength(10)]
        public string EditionNumber { get; set; }

        [Required]
        [Display(Name = "Registration begins date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Registration deadline")]
        public DateTime EndDate { get; set; }

        public string StatusMessage { get; set; }
    }
}
