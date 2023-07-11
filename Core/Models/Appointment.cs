using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Appointment: BaseModel
    {
        public enum AppoinmentStatus
        {
            [EnumMember(Value = "Pending")]
            Pending,
            [EnumMember(Value = "Confirm")]
            Confirm,
            [EnumMember(Value = "Done")]
            Done
        }
        public int Patient_id { get; set; }
        [ForeignKey("Patient_id")]
        public virtual Patient Patient { get; set; }

        public int ServiceDoctorId { get; set; }
        [ForeignKey("ServiceDoctorId")]
        [InverseProperty("Appointments")]
        public virtual ServiceDoctor ServiceDoctor { get; set; }

        public int Branch_id { get; set; }
        [ForeignKey("Branch_id")]
        public virtual Branch Branch { get; set; }

        public int emp_id { get; set; }
        [ForeignKey("emp_id")]
        public virtual Employee Employee { get; set; }

        [DataType(DataType.Date)]
        public string Date { get; set; }

        [DataType(DataType.Time)]
        public string TimeStart { get; set; }

        [DataType(DataType.Time)]
        public string TimeEnd { get; set; }
        public AppoinmentStatus Status { get; set; } = AppoinmentStatus.Pending;
    }
}

