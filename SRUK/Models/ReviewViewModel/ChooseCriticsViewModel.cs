using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Models
{
    public class ChooseCriticsViewModel
    {
        [Required]
        [Display(Name = "First critic")]
        public string FirstCriticId { get; set; }

        [Required]
        [Display(Name = "Second critic")]
        public string SecondCriticId { get; set; }

        public PaperVersionDTO PaperVersion { get; set; }
        public long PaperVersionId { get; set; }

        public DateTime Deadline { get; set; }

        public string StatusMessage { get; set; }
    }
}
