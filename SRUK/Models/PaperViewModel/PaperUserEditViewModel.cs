using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperUserEditViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public long Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string StatusMessage { get; set; }
    }
}
