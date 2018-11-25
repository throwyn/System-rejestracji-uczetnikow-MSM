using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ParticipancyShortDTO
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public UserDTO User { get; set; }

        [Required]
        public SeasonDTO Season { get; set; }

        [Required]
        public bool ConferenceParticipation { get; set; }

        [Required]
        public bool Publication { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public IEnumerable<PaperShortDTO> Papers { get; set; }
    }
}
