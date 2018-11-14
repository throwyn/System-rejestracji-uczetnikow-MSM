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

        public bool? EditorialErrors { get; set; }
        public bool? TechnicalErrors { get; set; }
        public bool? RepeatReview { get; set; }
        public bool? IsPositive { get; set; }
        public bool? IsPulp { get; set; }

        public string FileName { get; set; }
        public string OriginalFileName { get; set; }

        public string Comment { get; set; }

        [Required]
        public byte Status { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public DateTime CompletionDate { get; set; }

        [Required]
        public DateTime EditDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
