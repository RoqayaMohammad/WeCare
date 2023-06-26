using Core.Models;

namespace API.Dtos
{
    public class EmployeeDto
    {
       // public int Id { get; set; }
        public string jobTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public decimal Salary { get; set; }

        public string DepartementName { get; set; }

        
    }
}
