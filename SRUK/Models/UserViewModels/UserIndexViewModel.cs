using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class UserIndexViewModel
    {
        public List<UserShortDTO> User { get; set; }

        public string StatusMessage { get; set; }
    }
}
