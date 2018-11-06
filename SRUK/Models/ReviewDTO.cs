using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ReviewDTO
    {
        public long Id { get; set; }
        
        public UserDTO Critic { get; set; }
        //public string CriticId { get; set; }
        
        public PaperVersionDTO PaperVersion { get; set; }
        //public long PaperVersionId { get; set; }


        //public PaperDTO Paper { get; set; }

        //public UserDTO Author { get; set; }

        //public SeasonDTO Season { get; set; }

        public bool EditorialErrors { get; set; }
        public bool TechnicalErrors { get; set; }
        public bool RepeatReview { get; set; }
        public bool IsPositive { get; set; }
        public bool IsPulp { get; set; }
        
        public string FileName { get; set; }

        public string Comment { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
