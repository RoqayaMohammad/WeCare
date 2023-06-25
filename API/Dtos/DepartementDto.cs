using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class DepartementDto
    {
        [Key]
        public int Dept_ID { get; set; }
        public string Name { get; set; }

    }
}
