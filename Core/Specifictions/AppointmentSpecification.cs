using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifictions
{
    public class AppointmentSpecification:BaseSpecification<Appointment>
    {
        public AppointmentSpecification(string PatientName):base(o=>o.Patient.FName==PatientName)
        {
            AddInclude(o => o.Branch);
            AddInclude(o => o.Employee);
        
        }

        public AppointmentSpecification(int id,string PatientName):base(o=>o.Id==id && o.Patient.FName==PatientName)
        {

            AddInclude(o => o.Branch);
            AddInclude(o => o.Employee);

        }
    }

    //public OrderWithItemsAndOrderingSpecifications(string email) : base(o => o.BuyerEmail == email)
    //{
    //    AddInclude(o => o.OrderItems);
    //    AddInclude(o => o.DeliveryMethod);
    //    AddOrderByDes(o => o.OrderDate);
    //}

    //public OrderWithItemsAndOrderingSpecifications(int id, string email)
    //    : base(o => o.Id == id && o.BuyerEmail == email)
    //{
    //    AddInclude(o => o.OrderItems);
    //    AddInclude(o => o.DeliveryMethod);
    //}
}
