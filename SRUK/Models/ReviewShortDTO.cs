using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ReviewShortDTO
    {
        public long Id { get; set; }
        
        public UserDTO Critic { get; set; }
        
        public PaperVersionDTO PaperVersion { get; set; }

        public bool EditorialErrors { get; set; }
        public bool TechnicalErrors { get; set; }
        public bool RepeatReview { get; set; }
        public bool IsPositive { get; set; }
        public bool IsPulp { get; set; }
        
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }

        public string Comment { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }
        
        //public DateTime EditDate { get; set; }
        
        //public bool IsDeleted { get; set; }
    }
}
