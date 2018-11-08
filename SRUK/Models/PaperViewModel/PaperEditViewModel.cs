using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperEditViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public long Id { get; set; }
        
        [Display(Name = "Author")]
        public string AuthorId { get; set; }
        public UserDTO Author { get; set; }
        
        [Display(Name = "Season")]
        public long SeasonId { get; set; }
        public SeasonDTO Season { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public short Status { get; set; }

        public string StatusMessage { get; set; }
    }
}
