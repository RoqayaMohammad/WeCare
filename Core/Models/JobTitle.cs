using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class JobTitle :BaseModel
    {
        [Required(ErrorMessage = "You must enter the job title name")]
        [MaxLength(30, ErrorMessage = "Too Long Name!!")]
        [Display(Name = "Job Title")]
        public string? Title { get; set; }
    }
}
