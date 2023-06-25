using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class DoctorDto
    {
        [Key]
        public int Doctor_ID { get; set; }

        [Display(Name = "First Name")]
        public string? FName { get; set; }

        [Display(Name = "Last Name")]
        public string? LName { get; set; }
        public string? Address { get; set; }

        public string? Phone1 { get; set; }

        public string? Phone2 { get; set; }

        public DateTime? BirthOfDate { get; set; }

        public DateTime? HiringDate { get; set; }
        public int? Salary { get; set; }
        public string departement { get; set; }
    }
}
