using Core.Models;
using System.ComponentModel.DataAnnotations;
using static Core.Models.Appointment;

namespace API.Dtos
{
    public class AppointmentDto
    {
        public int AppId { get; set; }
        public string PatientName { get; set; }

        public string BranchName { get; set; }

        public string EmployeeName { get; set; }

        public string Date { get; set; }

        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public AppoinmentStatus Status { get; set; } = AppoinmentStatus.Pending;
        public string service { get; set; }
        public string DoctorName { get; set; }

    }
}
