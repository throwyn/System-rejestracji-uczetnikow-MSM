using EntityFrameworkPaginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class UserIndexViewModel : Page<UserShortDTO>
    {
        public short SortBy { get; set; }
        public string Degree { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organisation { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public string StatusMessage { get; set; }
    }
}
