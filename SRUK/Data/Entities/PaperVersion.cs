using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Entities
{
    public class PaperVersion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required]
        public Paper Paper { get; set; }
        public long PaperId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string OriginalFileName { get; set; }

        [Required]
        public byte Status { get; set; }


        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime EditDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public string Comment { get; set; }


        public ICollection<Comment> Comments { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
