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
        
        public string Name { get; set; }

        public string MainImageFileName { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime ConferenceStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime ConferenceEndDate { get; set; }
        
        public string Location { get; set; }
        
        public string EditionNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime EndDate { get; set; }
    }
}
