using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class UserIndexViewModel
    {
        public List<UserShortDTO> User { get; set; }

        public string SortOrder { get; set; }

        public string SearchDegree { get; set; }

        public string SearchFirstName { get; set; }

        public string SearchLastName { get; set; }

        public string SearchOrganisation { get; set; }

        public string SearchEmail { get; set; }

        public string SearchRole { get; set; }

        public string StatusMessage { get; set; }
    }
}
