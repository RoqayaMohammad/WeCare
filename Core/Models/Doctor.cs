using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class Doctor : BaseModel
    {
        [Required(ErrorMessage = "You must enter the first name")]
        [MaxLength(30, ErrorMessage = "Too Long Name!!")]
        [Display(Name = "First Name")]
        public string? FName { get; set; }

        [Required(ErrorMessage = "You must enter the last name")]
        [MaxLength(30, ErrorMessage = "Too Long Name!!")]
        [Display(Name = "Last Name")]
        public string? LName { get; set; }

        [Required(ErrorMessage = "You must enter the address")]
        [MaxLength(30, ErrorMessage = "Too Long Name!!")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "You must enter the Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? Phone1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? Phone2 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BirthOfDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? HiringDate { get; set; }

        [Required(ErrorMessage = "You must enter the Salary")]
        [DataType(DataType.Currency)]
        public int? Salary { get; set; }

        public Departement? departement { get; set; }
        public int DeptID { get; set; }

        public string GetFullName()
        {
            return $"{FName}, {LName}";
        }
    }
}
