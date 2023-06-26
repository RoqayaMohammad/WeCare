using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class BranchDoctorDto
    {
        [Key]
        public int branchDoctor_ID { get; set; }
        public string doctorName { get; set; }
        public string branchName { get; set; }
        public string notes { get; set; }

    }
}
