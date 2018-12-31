using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ParticipancySignUpViewModel
    {
        public string SeasonName { get; set; }

        public string UserId { get; set; }

        [Required]
        [Display(Name = "Participation in conference")]
        public bool ConferenceParticipation { get; set; }

        [Required]
        [Display(Name = "Paper publication")]
        public bool Publication { get; set; }

        public DateTime Deadline { get; set; }

        public string StatusMessage { get; set; }
    }
}
