using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BranchDept:BaseModel
    {

        public int Branch_id { get; set; }
        public Branch? Branch { get; set; }
        public int Dept_id { get; set; }
        public Departement? Departement { get; set; }

        public string? Notes { get; set; }
    }
}

//branch_id
//    =======
//    dep_id
//    ======
//    notes
