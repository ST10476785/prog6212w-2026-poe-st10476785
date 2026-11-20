using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RaceDayMVC.Models
{
    public class ResultViewModelcs // ResultViewModel class represents the data model for a result in the RaceDayMVC for event results
    {
        public int ResultId { get; set; }

        [Required]
        [DisplayName("Finishing Time")]
        public TimeOnly FinishingTime { get; set; }

        [Required]
        [DisplayName("Finishing Position")]
        public int FinishingPosition { get; set; }

        [DisplayName("Result Date")]
        [DataType(DataType.Date)]
        public DateTime ResultDate { get; set; } = DateTime.Now;

        [Required]
        public int EventEnrollmentId { get; set; }
    }
}