using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class BranchDoctorSpecification:BaseSpecification<BranchDoctor>
    {
        public BranchDoctorSpecification()
        {
            AddInclude(x => x.doctor);
            AddInclude(x => x.branch);


        }
        public BranchDoctorSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.doctor);
            AddInclude(x => x.branch);
        }
    }
}
