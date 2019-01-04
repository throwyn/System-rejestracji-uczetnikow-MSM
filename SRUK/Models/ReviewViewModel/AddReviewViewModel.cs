using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SRUK.Models
{
    public class AddReviewViewModel
    {
        [Required]
        public long Id { get; set; }

        public PaperVersionDTO PaperVersion { get; set; }

        [Required]
        [Display(Name = "Recommendation")]
        public byte Recommendation { get; set; }

        [Required]
        [Display(Name = "Review file")]
        public IFormFile File { get; set; }

        [Display(Name = "Comment for administration")]
        public string CommentForAdmin { get; set; }

        [Display(Name = "Comment for author")]
        public string CommentForAuthor { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }

        public string StatusMessage { get; set; }
    }
}
