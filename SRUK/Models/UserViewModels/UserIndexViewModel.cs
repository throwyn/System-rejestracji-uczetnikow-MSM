using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class UserIndexViewModel
    {
        public List<UserShortDTO> User { get; set; }

        int PageNumber { get; set; }

        int PageSize { get; set; }

        int NumberOfPages { get; set; }

        public string StatusMessage { get; set; }
    }
}
