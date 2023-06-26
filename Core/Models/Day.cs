using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class Day:BaseModel
    {
        [Required(ErrorMessage = "You must enter the day name")]
        [MaxLength(20, ErrorMessage = "Too Long Name!!")]
        [Display(Name = "Day Name")]
        public string Name { get; set; }
    }
}
