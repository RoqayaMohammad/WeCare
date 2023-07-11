using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class AppoinmentSpecParams
    {
        public int? patientID { get; set; }
        public int? branchID { get; set; }
        public int? employeeID { get; set; }
        public int? doctorId { get; set; }
        public int? serviceId { get; set; }
        private string _date { get; set; }
        public string? Datesearch { get => _date; set => _date = value.ToLower(); }
    }
}
