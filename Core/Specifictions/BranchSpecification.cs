using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class BranchSpecification :BaseSpecification<Branch>
    {
        public BranchSpecification() 
        {
            AddInclude(x => x.weekend);

        }
        public BranchSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.weekend);

        }
    }
}
