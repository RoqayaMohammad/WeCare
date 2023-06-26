using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BranchDoctor :BaseModel
    {
        public Doctor? doctor { get; set; }
        public int doctorID { get; set; }
        public Branch? branch { get; set; }
        public int branchID { get; set; }
        public string notes { get; set; }

    }
}
