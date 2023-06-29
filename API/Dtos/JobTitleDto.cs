using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class JobTitleDto
    {
        [Key]
        public int job_ID { get; set; }
        public string Title { get; set; }
    }
}
