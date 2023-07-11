using Core.Models;

namespace API.Dtos
{
    public class ServiceDoctorDto
    {
        public int servDoctorId { get; set; }
        public string? ServiceName { get; set; }
        public string? DoctorName { get; set; }
        public string? BranchName { get; set; }
        public string Notes { get; set; }

    }
}
