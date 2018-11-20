using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRUK.Models
{
    public class AcademicDegrees
    {
        public List<SelectListItem> SelectListItems {get { return new List <SelectListItem>
            {
                new SelectListItem { Text = "", Value = "" },
                new SelectListItem { Text = "Prof. DSc. PhD. Eng.", Value = "Prof. DSc. PhD. Eng." },
                new SelectListItem { Text = "Prof. DSc. PhD.", Value = "Prof. DSc. PhD." },
                new SelectListItem { Text = "DSc. PhD. Eng.", Value = "DSc. PhD. Eng." },
                new SelectListItem { Text = "DSc. PhD.", Value = "DSc. PhD." },
                new SelectListItem { Text = "PhD. Eng.", Value = "PhD. Eng." },
                new SelectListItem { Text = "PhD.", Value = "PhD." },
                new SelectListItem { Text = "MSc. Eng.", Value = "MSc. Eng." },
                new SelectListItem { Text = "MSc.", Value = "MSc." },
                new SelectListItem { Text = "PE", Value = "PE" }
            };
            }
        }
    }
}
