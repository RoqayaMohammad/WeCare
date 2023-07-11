using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class AppoinmentWithSpecification : BaseSpecification<Appointment>
    {
        public AppoinmentWithSpecification(AppoinmentSpecParams appParams) : base(x =>
        (!appParams.doctorId.HasValue || x.ServiceDoctor.BranchDoctor.doctorID == appParams.doctorId) &&
        (!appParams.branchID.HasValue || x.Branch_id == appParams.branchID) &&
        (!appParams.employeeID.HasValue || x.emp_id == appParams.employeeID) &&
        (!appParams.patientID.HasValue || x.Patient_id == appParams.patientID) &&
        (!appParams.serviceId.HasValue || x.ServiceDoctor.serv_id == appParams.serviceId) &&
        (string.IsNullOrEmpty(appParams.Datesearch) || x.Date.ToLower().Contains(appParams.Datesearch))
)
        {
            AddInclude(x => x.ServiceDoctor);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Employee);
            AddInclude(x => x.Patient);
        }
        public AppoinmentWithSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ServiceDoctor);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Employee);
            AddInclude(x => x.Patient);
        }
    }
}
