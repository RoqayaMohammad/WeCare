using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class DoctorShiftSpecification:BaseSpecification<DoctorShift>
    {
        public DoctorShiftSpecification(DoctorShiftSpecParams doctorParams) : base(x =>
        (!doctorParams.doctorId.HasValue || x.branchDoctor.doctorID == doctorParams.doctorId) &&
        (!doctorParams.branchId.HasValue || x.branchDoctor.branchID == doctorParams.branchId) &&
        (!doctorParams.dayId.HasValue || x.day_ID == doctorParams.dayId) 

        )
        {
            AddInclude(x => x.branchDoctor);
            AddInclude(x => x.day);


        }
        public DoctorShiftSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.branchDoctor);
            AddInclude(x => x.day);
        }
    }
}
