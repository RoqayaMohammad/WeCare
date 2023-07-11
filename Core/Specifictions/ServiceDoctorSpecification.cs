using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class ServiceDoctorSpecification : BaseSpecification<ServiceDoctor>
    {
        public ServiceDoctorSpecification()
        {
            AddInclude(x => x.Service);
            AddInclude(x => x.Appointments);
            AddInclude(x => x.BranchDoctor);


        }
        public ServiceDoctorSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Service);
            AddInclude(x => x.Appointments);
            AddInclude(x => x.BranchDoctor);
        }
    }
}
