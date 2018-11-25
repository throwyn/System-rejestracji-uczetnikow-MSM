using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperDTO
    {
        public long Id { get; set; }

        public ParticipancyDTO Participancy { get; set; }
        public long ParticipancyId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        public short Status { get; set; }

        public DateTime SentToPrintDate { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }

        public ICollection<PaperVersionDTO> PaperVersions { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
