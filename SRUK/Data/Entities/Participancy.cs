using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Entities
{
    public class Participancy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [Required]
        public Season Season { get; set; }
        public long SeasonId { get; set; }

        [Required]
        public bool ConferenceParticipation { get; set; }

        [Required]
        public bool Publication { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Paper> Papers { get; set; }
    }
}
