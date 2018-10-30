using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Entities
{
    public class Paper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }

        [Required]
        public Season Season { get; set; }
        public long SeasonId { get; set; }


        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        public short Status { get; set; }
        
        public bool IsPaid { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime EditDate { get; set; }

        public ICollection<PaperVersion> PaperVersions { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
