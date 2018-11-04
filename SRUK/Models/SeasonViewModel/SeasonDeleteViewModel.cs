using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class SeasonDeleteViewModel
    {
        public long Id { get; set; }
        
        public string MainImageFileName { get; set; }
        
        public string LogoFileName { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime EndDate { get; set; }
    }
}
