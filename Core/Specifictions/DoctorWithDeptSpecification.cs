using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class DoctorWithDeptSpecification : BaseSpecification<Doctor>
    {
        public DoctorWithDeptSpecification(DoctorSpecParams doctorParams) : base(x =>
        (string.IsNullOrEmpty(doctorParams.FNsearch) || x.FName.ToLower().Contains(doctorParams.FNsearch)) &&
        (string.IsNullOrEmpty(doctorParams.LNsearch) || x.LName.ToLower().Contains(doctorParams.LNsearch)) &&
        (!doctorParams.deptId.HasValue || x.DeptID == doctorParams.deptId)
        )

        {
            AddInclude(x => x.departement);

        }
        public DoctorWithDeptSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.departement);

        }
    }
}
