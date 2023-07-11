using Core.Models;

namespace API.Dtos
{
    public class ServiceDto
    {
        public int ServId { get; set; }
        public string DepartementName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
