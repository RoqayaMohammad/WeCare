using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EmpShift:BaseModel
    {
        public int Branch_emp_id { get; set; }
        public BrachEmp? BrachEmp { get; set; }
        public Day? day { get; set; }
        public int day_ID { get; set; }

        [DataType(DataType.Time)]
        public string StartTime { get; set; }
        [DataType(DataType.Time)]
        public string EndTime { get; set; }
    }
}
