using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class MyParticipanciesViewModel
    {
        public List<ParticipancyShortDTO> Participancies { get; set; }

        public string StatusMessage { get; set; }
    }
}
