using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ParticipancyDTO
    {
        public long Id { get; set; }
        
        public UserDTO User { get; set; }
        public string UserId { get; set; }

        public SeasonDTO Season { get; set; }
        public long SeasonId { get; set; }

        public bool ConferenceParticipation { get; set; }
        
        public bool Publication { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }
        
        public bool IsDeleted { get; set; }
        public ICollection<PaperDTO> Papers { get; set; }
    }
}
