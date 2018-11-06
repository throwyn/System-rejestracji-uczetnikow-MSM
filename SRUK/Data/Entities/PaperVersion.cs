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
        public short Status { get; set; }

        public ApplicationUser Author
        {
            get
            {
                return Paper.Author;
            }
        }
        public Season Season
        {
            get
            {
                return Paper.Season;
            }
        }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime EditDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


        public ICollection<Review> Reviews { get; set; }
    }
}
