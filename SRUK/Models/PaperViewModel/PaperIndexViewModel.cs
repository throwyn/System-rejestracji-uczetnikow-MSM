using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperIndexViewModel
    {
        public List<PaperShortDTO> Papers { get; set; }

        public string StatusMessage { get; set; }
    }
}
