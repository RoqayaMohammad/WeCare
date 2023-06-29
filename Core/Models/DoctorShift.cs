using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DoctorShift:BaseModel
    {
        public BranchDoctor? branchDoctor { get; set; }
        public int branchDoctor_ID { get; set; }
        public Day? day { get; set; }
        public int day_ID { get; set; }

        [DataType(DataType.Time)]
        public string? startTime { get; set; }

        [DataType(DataType.Time)]
        public string? endTime { get; set; }

    }
}
