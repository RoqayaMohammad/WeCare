using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class AppointmentDto
    {
        public int AppId { get; set; }
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "Brach Name")]
        public string BranchName { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Status { get; set; }


    }
}
