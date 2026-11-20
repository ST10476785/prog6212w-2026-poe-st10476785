using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RaceDayMVC.Models
{
    public class EventEnrollmentViewModel // EventEnrollmentViewModel class represents the data model for an event enrollment in the RaceDayMVC application
    {
        public int EventEnrollmentId { get; set; }

        [DisplayName("Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Active";

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}