using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class DoctorSpecParams
    {
        public int? deptId { get; set; }
        private string _fnsearch { get; set; }
        public string? FNsearch { get => _fnsearch; set => _fnsearch = value.ToLower(); }
        private string _lnsearch { get; set; }
        public string? LNsearch { get => _lnsearch; set => _lnsearch = value.ToLower(); }
    }
}
