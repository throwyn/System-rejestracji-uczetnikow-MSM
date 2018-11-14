using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperShortDTO
    {
        public long Id { get; set; }
        
        public UserDTO Author { get; set; }

        public string Title { get; set; }
        
        public short Status { get; set; }

        public byte VersionsNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }
    }
}
