using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Models
{
    public class PaperVersionDTO
    {
        public long Id { get; set; }
        
        public PaperDTO Paper { get; set; }
        public long PaperId { get; set; }
        
        public string FileName { get; set; }

        public string OriginalFileName { get; set; }

        public short Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }
        
        public bool IsDeleted { get; set; }

        public ICollection<ReviewDTO> Reviews { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }

    }
}
