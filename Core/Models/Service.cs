using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Service : BaseModel
    {
        public int Debt_id { get; set; }
        public virtual Departement Departement { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}

//serv_id
//    dept_id
//	name
//	price

