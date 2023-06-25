using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class PatientSpecification : BaseSpecification<Patient>
    {
        public PatientSpecification(PatientSpecParams patientParams) : base(x =>
        (string.IsNullOrEmpty(patientParams.FNsearch) || x.FName.ToLower().Contains(patientParams.FNsearch)) &&
        (string.IsNullOrEmpty(patientParams.LNsearch) || x.LName.ToLower().Contains(patientParams.LNsearch)) &&
        (string.IsNullOrEmpty(patientParams.PhoneSearch) || (x.Phone1.ToLower().Contains(patientParams.PhoneSearch) || x.Phone2.ToLower().Contains(patientParams.PhoneSearch)))
        )

        {

        }

    }
}
