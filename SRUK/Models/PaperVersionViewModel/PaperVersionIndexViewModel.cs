using EntityFrameworkPaginate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperVersionIndexViewModel : Page<PaperVersionShortDTO>
    {
        public short SortBy { get; set; }
        
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }

        public string StatusMessage { get; set; }
    }
}
