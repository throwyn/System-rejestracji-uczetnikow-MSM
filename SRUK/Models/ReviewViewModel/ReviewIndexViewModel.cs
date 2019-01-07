using EntityFrameworkPaginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ReviewIndexViewModel : Page<ReviewShortDTO>
    {
        public short SortBy { get; set; }
        
        public string Title { get; set; }
        public string FirstNameAuthor { get; set; }
        public string LastNameAuthor { get; set; }
        public string FirstNameCritic { get; set; }
        public string LastNameCritic { get; set; }
        public string Status { get; set; }

        public string StatusMessage { get; set; }
    }
}
