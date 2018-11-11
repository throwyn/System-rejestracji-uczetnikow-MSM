using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class CommentShortDTO
    {
        public long Id { get; set; }
        
        public PaperVersionDTO PaperVersion { get; set; }
        
        public UserDTO Author { get; set; }
        
        public string Content { get; set; }
        
        public DateTime CreationDate { get; set; }
    }
}
