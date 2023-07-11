using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Service : BaseModel
    {
        public int Dept_id { get; set; }
        public Departement? Departement { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}


