using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class Departement :BaseModel
    {
        [Required(ErrorMessage = "You must enter the departement name")]
        [MaxLength(30, ErrorMessage = "Too Long Name!!")]
        [Display(Name = "Dept Name")]
        public string? Name { get; set; }
    }
}
