using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EmpShift:BaseModel
    {
        public int Branch_emp_id { get; set; }
        public virtual BrachEmp BrachEmp { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

//branch_emp_id
//    ========
//    day_of_week_id
//    ========
//    start_time

//    end_time
