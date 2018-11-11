using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class PaperVersionIndexViewModel
    {
        public List<PaperVersionShortDTO> Versions { get; set; }

        public string StatusMessage { get; set; }
    }
}
