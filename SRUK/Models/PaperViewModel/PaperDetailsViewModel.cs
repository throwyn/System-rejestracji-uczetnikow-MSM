using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperDetailsViewModel
    {
        [Display(Name = "ID")]
        public long Id { get; set; }
        public UserDTO Author { get; set; }
        public SeasonDTO Season { get; set; }
        public string EndSeason { get { return Season.EndDate.Year.ToString(); } }
        public string Title { get; set; }
        public short Status { get; set; }
        
        public ICollection<PaperVersionDTO> PaperVersions { get; set; }

        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }
        public string StatusMessage { get; set; }
    }
}
