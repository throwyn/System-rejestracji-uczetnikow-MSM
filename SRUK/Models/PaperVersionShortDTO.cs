﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRUK.Models
{
    public class PaperVersionShortDTO
    {
        public long Id { get; set; }
        
        public PaperDTO Paper { get; set; }
        
        public short Status { get; set; }

        public string OriginalFileName { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime CreationDate { get; set; }
    }
}
