﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class SeasonDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string MainImageFileName { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public DateTime ConferenceStartDate { get; set; }
        
        public DateTime ConferenceEndDate { get; set; }
        
        public string Location { get; set; }
        
        public string EditionNumber { get; set; }

        public DateTime CreationDate { get; set; }
        
        public DateTime EditDate { get; set; }
        
        public bool IsDeleted { get; set; }

        public ICollection<ParticipancyDTO> Participancies { get; set; }
    }
}
