using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperCreateViewModel
    {
        [Required]
        [Display(Name = "Author")]
        public string AuthorId { get; set; }

        [Required]
        [Display(Name = "Season")]
        public long SeasonId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public short Status { get; set; }

        [Required]
        public long ParticipancyId { get; set; }

        public string StatusMessage { get; set; }
    }
}
