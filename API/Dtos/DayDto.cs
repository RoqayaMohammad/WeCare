using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class DayDto
    {
        [Key]
        public int Day_ID { get; set; }
        public string Name { get; set; }
    }
}
