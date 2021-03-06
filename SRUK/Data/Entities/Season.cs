﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Entities
{
    public class Season
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
   
        [Required]
        public string MainImageFileName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime ConferenceStartDate { get; set; }

        [Required]
        public DateTime ConferenceEndDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string EditionNumber { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Participancy> Participancies { get; set; }
    }
}
