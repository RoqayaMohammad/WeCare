﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Employee: BaseModel
    {
        
            public string jobTitle { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public int Age { get; set; }
            public string Phone1 { get; set; }
            public string Phone2 { get; set; }
            public decimal Salary { get; set; }

            public Departement Departement { get; set; }
            public int DeptId { get; set; }


        
    }
}