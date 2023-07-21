using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    internal class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        public int? AppEmpId { get; set; }
        public AppEmp? Appemp { get; set; }

        public int? doctrorId { get; set; }
        public Doctor? doctor { get; set; }
    }
}
