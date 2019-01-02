using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ReviewDTO
    {
        public long Id { get; set; }
        
        public UserDTO Critic { get; set; }
        public string CriticId { get; set; }

        public PaperVersionDTO PaperVersion { get; set; }
        public long PaperVersionId { get; set; }

        public byte Recommendation { get; set; }

        public string FileName { get; set; }
        public string OriginalFileName { get; set; }

        public string CommentForAuthor { get; set; }
        public string CommentForAdmin { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CompletionDate { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
