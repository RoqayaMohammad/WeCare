using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class DoctorShiftDto
    {
        [Key]
        public int DoctorShift_ID { get; set; }
        public string DoctorName { get; set; }
        public string BranchName { get; set; }
        public string DayOfWeek { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}
