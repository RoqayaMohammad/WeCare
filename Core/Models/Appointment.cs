using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Appointment: BaseModel
    {

        public int Patient_id { get; set; }
        public virtual Patient Patient { get; set; }
        public int Serv_doctor_id { get; set; }
        public virtual ServiceDoctor ServiceDoctor { get; set; }
        public int Branch_id { get; set; }
        public virtual Branch Branch { get; set; }
        public int emp_id { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime Date { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Status { get; set; }
    }
}



//appoinment_id
//patiend_id
//serv_doctor_id
//branch_id
//emp_id
//date
//time_start
//time_end
//status
