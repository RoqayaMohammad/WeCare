using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class EmpSpecParams
    {
        private string _fnsearch { get; set; }
        public string? FNsearch { get => _fnsearch; set => _fnsearch = value.ToLower(); }
        private string _lnsearch { get; set; }
        public string? LNsearch { get => _lnsearch; set => _lnsearch = value.ToLower(); }
        private string _Phonesearch { get; set; }
        public string? PhoneSearch { get => _Phonesearch; set => _Phonesearch = value.ToLower(); }
        public int? jobId { get; set; }

    }
}
