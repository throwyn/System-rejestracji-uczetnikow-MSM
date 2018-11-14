using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ReviewIndexViewModel
    {
        public List<ReviewShortDTO> Reviews { get; set; }

        public string StatusMessage { get; set; }
    }
}
