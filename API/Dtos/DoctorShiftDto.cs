using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class DoctorShiftDto
    {
        [Key]
        public int DoctorShift_ID { get; set; }
        public string DoctorName { get; set; }
        public string BranchName { get; set; }
        public string dayOfWeek { get; set; }
        public string? startTime { get; set; }
        public string? endTime { get; set; }
    }
}
