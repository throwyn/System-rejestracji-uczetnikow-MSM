﻿using System;
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

        [Display(Name = "Main image filename")]
        [StringLength(100,MinimumLength = 5)]
        public string MainImageFileName { get; set; }

        [Display(Name = "Logo image filename")]
        [StringLength(100, MinimumLength = 5)]
        public string LogoFileName { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; }

        public string StatusMessage { get; set; }
    }
}
