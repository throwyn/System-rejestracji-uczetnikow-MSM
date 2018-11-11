using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperVersionsAddViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public long PaperId { get; set; }

        [Required]
        [Display(Name = "Paper file")]
        public IFormFile File { get; set; }

        public string StatusMessage { get; set; }
    }
}
