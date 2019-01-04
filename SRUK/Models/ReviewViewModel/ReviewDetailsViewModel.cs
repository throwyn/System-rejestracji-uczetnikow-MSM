using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Models
{
    public class ReviewDetailsViewModel
    {
        public long Id { get; set; }

        public UserDTO Critic { get; set; }
        public string CriticId { get; set; }

        public PaperVersionDTO PaperVersion { get; set; }
        public long PaperVersionId { get; set; }
        
        [Display(Name = "Recommendation")]
        public byte Recommendation { get; set; }

        [Display(Name = "System filename")]
        public string FileName { get; set; }

        [Display(Name ="Original filename")]
        public string OriginalFileName { get; set; }

        [Display(Name = "Comment for author")]
        public string CommentForAuthor { get; set; }

        [Display(Name = "Comment for admin")]
        public string CommentForAdmin { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Display(Name = "Date of choosing critic")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime Deadline { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        [Display(Name = "Completion date")]
        public DateTime CompletionDate { get; set; }

        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }

        public string StatusMessage { get; set; }
    }
}
