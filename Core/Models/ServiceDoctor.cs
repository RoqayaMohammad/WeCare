using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ServiceDoctor: BaseModel
    {
        public int serv_id { get; set; }
        public Service? Service { get; set; }
        public int Branch_doctor_id { get; set; }
        public BranchDoctor? BranchDoctor { get; set; }
        public string Notes { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}

