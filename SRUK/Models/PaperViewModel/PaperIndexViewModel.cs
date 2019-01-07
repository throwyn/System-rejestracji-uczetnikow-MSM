using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkPaginate;

namespace SRUK.Models
{
    public class PaperIndexViewModel : Page<PaperShortDTO>
    {
        public short SortBy { get; set; }

        public string Season { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }

        public string StatusMessage { get; set; }
    }
}
