using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class SeasonIndexViewModel
    {
        public List<SeasonShortDTO> Seasons { get; set; }

        public string StatusMessage { get; set; }
    }
}
