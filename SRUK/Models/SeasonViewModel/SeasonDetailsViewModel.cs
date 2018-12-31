using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class SeasonDetailsViewModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        [Display(Name = "Main image filename")]
        public string MainImageFileName { get; set; }

        [Display(Name = "Start date of conf.")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime ConferenceStartDate { get; set; }

        [Display(Name = "End date of conf.")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime ConferenceEndDate { get; set; }

        [Display(Name = "Locations")]
        public string Location { get; set; }

        [Display(Name = "Number of edition")]
        public string EditionNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Display(Name = "Registration begins")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Display(Name = "Registration deadline")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Season created")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Is deleted")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public bool IsDeleted { get; set; }
    }
}
