using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Entities
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required]
        public ApplicationUser Critic { get; set; }
        public string CriticId { get; set; }
        
        [Required]
        public PaperVersion PaperVersion { get; set; }
        public long PaperVersionId { get; set; }


        public Paper Paper
        {
            get
            {
                return PaperVersion.Paper;
            }
        }

        public ApplicationUser Author
        {
            get
            {
                return PaperVersion.Author;
            }
        }

        public Season Season
        {
            get
            {
                return PaperVersion.Season;
            }
        }

        public bool EditorialErrors { get; set; }
        public bool TechnicalErrors { get; set; }
        public bool RepeatReview { get; set; }
        public bool IsPositive { get; set; }
        public bool IsPulp { get; set; }

        [Required]
        public string FileName { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime EditDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
