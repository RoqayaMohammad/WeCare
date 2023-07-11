using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class ServiceSpecification: BaseSpecification<Service>
    { 
            public ServiceSpecification()
            {
                AddInclude(x => x.Departement);

            }
            public ServiceSpecification(int id) : base(x => x.Id == id)
            {
                AddInclude(x => x.Departement);

            }
        }
    }

