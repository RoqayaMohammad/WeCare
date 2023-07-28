using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class PatientDto
    {
        [Key]
        public int Patient_ID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Address { get; set; }

        public string? Phone1 { get; set; }

        public string? Phone2 { get; set; }

        public DateTime? BirthOfDate { get; set; }
    }
}
