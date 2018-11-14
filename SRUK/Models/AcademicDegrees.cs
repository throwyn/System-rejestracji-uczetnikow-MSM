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
                new SelectListItem { Text = "Doctor of Engineering", Value = "D.Eng." },
                new SelectListItem { Text = "Master of Engineering", Value = "M.Eng." },
                new SelectListItem { Text = "Bachelor of Engineering", Value = "B.Eng" },
                new SelectListItem { Text = "Professor", Value = "Prof." },
                new SelectListItem { Text = "Master of Science", Value = "M.Sc." },
                new SelectListItem { Text = "Bachelor of Sciene", Value = "B.Sc." },
                new SelectListItem { Text = "Doctor of Philosophy", Value = "Ph.D." },
                new SelectListItem { Text = "Master of Arts", Value = "M.A" },
                new SelectListItem { Text = "Master of Business Administration", Value = "MBA" },
                new SelectListItem { Text = "Master of Education", Value = "M.Ed." },
                new SelectListItem { Text = "Bachelor of Arts ", Value = "B.A" },
                new SelectListItem { Text = "Bachelor of Education", Value = "B.Ed." },
                new SelectListItem { Text = "University student", Value = "" }
            };
            }
        }
    }
}
