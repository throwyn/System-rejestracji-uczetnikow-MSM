using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class SeasonEditViewModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Main image filename")]
        [StringLength(100,MinimumLength = 5)]
        public string MainImageFileName { get; set; }

        [Required]
        [Display(Name = "Start date of conference")]
        [DataType(DataType.DateTime)]
        public DateTime ConferenceStartDate { get; set; }

        [Required]
        [Display(Name = "End date of conference")]
        [DataType(DataType.DateTime)]
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
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Registration deadline")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }

        public string StatusMessage { get; set; }
    }
}
