using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class SeasonShortDTO
    {
        public long Id { get; set; }

        public string MainImageFileName { get; set; }
        
        public string LogoFileName { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
