using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class Branch : BaseModel
    {
        [Required(ErrorMessage = "You must enter the name")]
        [MaxLength(30, ErrorMessage = "Too Long Name!!")]
        [Display(Name = "Branch Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter the address")]
        [MaxLength(30, ErrorMessage = "Too Long Name!!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must enter the Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone2 { get; set; }
        [DataType(DataType.Time)]
        public string openTime { get; set; }

        [DataType(DataType.Time)]
        public string closeTime { get; set; }
        public Day weekend { get; set; }
        public int weekendID { get; set; }

    }
}
