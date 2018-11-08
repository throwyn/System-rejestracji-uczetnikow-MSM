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
        
        public UserDTO Author { get; set; }
        public string AuthorId { get; set; }
        
        public SeasonDTO Season { get; set; }
        public long SeasonId { get; set; }


        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        public short Status { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }

        public ICollection<PaperVersionDTO> PaperVersions { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
