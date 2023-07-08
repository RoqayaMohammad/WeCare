using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BrachEmp:BaseModel
    {

        public int Branch_id { get; set; }
        public virtual Branch Branch { get; set; }
        public int Emp_id { get; set; }
        public virtual Employee Employee { get; set; }

        public string Notes { get; set; }
    }
}

//branch_id
//    =======
//    emp_id
//    ======
//    notes
