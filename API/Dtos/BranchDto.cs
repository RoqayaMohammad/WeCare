using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BranchDto
    {
        [Key]
        public int Branch_ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Address { get; set; }

        public string Phone1 { get; set; }

        public string? Phone2 { get; set; }

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }
        public string WeekendDay { get; set; }
    }
}
