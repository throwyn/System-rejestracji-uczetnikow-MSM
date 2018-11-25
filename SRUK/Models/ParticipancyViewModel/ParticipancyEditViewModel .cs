using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ParticipancyEditViewModel
    {
        [Required]
        public long Id { get; set; }
        
        public string SeasonName { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Participation in conference")]
        public bool ConferenceParticipation { get; set; }

        [Required]
        [Display(Name = "Paper publication")]
        public bool Publication { get; set; }

        public string StatusMessage { get; set; }
    }
}
