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

        [Required]
        public UserDTO Author { get; set; }
        public string AuthorId { get; set; }

        [Required]
        public SeasonDTO Season { get; set; }
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

        public ICollection<PaperVersionDTO> PaperVersions { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
