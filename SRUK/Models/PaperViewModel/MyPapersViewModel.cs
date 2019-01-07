using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkPaginate;

namespace SRUK.Models
{
    public class MyPapersViewModel
    {
        public List<PaperShortDTO> Papers { get; set; }
        public string StatusMessage { get; set; }
    }
}
