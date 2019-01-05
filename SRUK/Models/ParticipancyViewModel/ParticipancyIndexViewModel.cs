using EntityFrameworkPaginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class ParticipancyIndexViewModel : Page<ParticipancyShortDTO>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? ConferenceParticipation { get; set; }
        public bool? Publication { get; set; }
        public string SortBy { get; set; }
        public string Season { get; set; }

        public string StatusMessage { get; set; }
    }
}
