using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperVersionsRejectViewModel
    {

        public long Id { get; set; }

        public PaperDTO Paper { get; set; }

        public string FileName { get; set; }

        [Display(Name = "Filename")]
        public string OriginalFileName { get; set; }

        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }
    }
}
