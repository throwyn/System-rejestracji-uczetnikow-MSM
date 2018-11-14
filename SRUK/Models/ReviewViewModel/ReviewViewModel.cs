using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Models
{
    public class ReviewViewModel
    {
        public long Id { get; set; }

        public PaperVersionDTO PaperVersion { get; set; }
        public long PaperVersionId { get; set; }
        
        [Display(Name = "Editorial errors")]
        public bool EditorialErrors { get; set; }
        
        [Display(Name = "Technical errors")]
        public bool TechnicalErrors { get; set; }
        
        [Display(Name = "Repeat review")]
        public bool RepeatReview { get; set; }
        
        [Display(Name = "Is positive")]
        public bool IsPositive { get; set; }
        
        [Display(Name = "Completely unsuitable")]
        public bool IsPulp { get; set; }

        [Display(Name = "System filename")]
        public string FileName { get; set; }

        [Display(Name ="Original filename")]
        public string OriginalFileName { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Date of choosing critic")]
        public DateTime CreationDate { get; set; }

        public DateTime EditDate { get; set; }

        public string StatusMessage { get; set; }
    }
}
