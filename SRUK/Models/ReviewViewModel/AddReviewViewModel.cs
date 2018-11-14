using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SRUK.Models
{
    public class AddReviewViewModel
    {
        [Required]
        public long Id { get; set; }

        public PaperVersionDTO PaperVersion { get; set; }

        [Required]
        [Display(Name = "Editorial errors")]
        public bool EditorialErrors { get; set; }

        [Required]
        [Display(Name = "Technical errors")]
        public bool TechnicalErrors { get; set; }

        [Required]
        [Display(Name = "Repeat review")]
        public bool RepeatReview { get; set; }

        [Required]
        [Display(Name = "Is positive")]
        public bool IsPositive { get; set; }

        [Required]
        [Display(Name = "Completely unsuitable")]
        public bool IsPulp { get; set; }

        [Required]
        [Display(Name = "Review file")]
        public IFormFile File { get; set; }
        
        public string Comment { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }
        public string StatusMessage { get; set; }
    }
}
