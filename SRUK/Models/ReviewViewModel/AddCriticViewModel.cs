using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Models
{
    public class AddCriticViewModel
    {
        [Required]
        [Display(Name = "Critic")]
        public string CriticId { get; set; }

        public PaperVersionDTO PaperVersion { get; set; }
        public long PaperVersionId { get; set; }

        public DateTime Deadline { get; set; }

        public string StatusMessage { get; set; }
    }
}
