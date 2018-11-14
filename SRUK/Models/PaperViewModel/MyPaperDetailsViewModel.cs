using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class MyPaperDetailsViewModel
    {
        public long Id { get; set; }

        public SeasonDTO Season { get; set; }
        public string Title { get; set; }
        public short Status { get; set; }
        
        public ICollection<PaperVersionDTO> PaperVersions { get; set; }
        public string StatusMessage { get; set; }
    }
}
