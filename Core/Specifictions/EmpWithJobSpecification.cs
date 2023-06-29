using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class EmpWithJobSpecification : BaseSpecification<Employee>
    {
        public EmpWithJobSpecification(EmpSpecParams empParams) : base(x =>
        (string.IsNullOrEmpty(empParams.FNsearch) || x.FirstName.ToLower().Contains(empParams.FNsearch)) &&
        (string.IsNullOrEmpty(empParams.LNsearch) || x.LastName.ToLower().Contains(empParams.LNsearch)) &&
        (string.IsNullOrEmpty(empParams.PhoneSearch) || (x.Phone1.ToLower().Contains(empParams.PhoneSearch) || x.Phone2.ToLower().Contains(empParams.PhoneSearch))) &&
        (!empParams.jobId.HasValue || x.jobID == empParams.jobId)

        )

        {
            AddInclude(x => x.jobTitle);

        }
        public EmpWithJobSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.jobTitle);

        }

    }
}
