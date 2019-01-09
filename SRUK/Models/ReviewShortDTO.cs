using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ReviewShortDTO
    {
        public long Id { get; set; }
        
        public UserDTO Critic { get; set; }
        
        public PaperVersionDTO PaperVersion { get; set; }

        public byte Recommendation { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime Deadline { get; set; }
        
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }

        public string CommentForAuthor { get; set; }
        public string CommentForAdmin { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CompletionDate { get; set; }
    }
}
