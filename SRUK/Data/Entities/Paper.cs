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
        public Participancy Participancy { get; set; }
        public long ParticipancyId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        public byte Status { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime EditDate { get; set; }
        public DateTime SentToPrintDate { get; set; }

        public virtual ICollection<PaperVersion> PaperVersions { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
