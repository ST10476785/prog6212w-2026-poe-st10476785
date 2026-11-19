
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RaceDayMVC.Models
{ // ActivityViewModel class represents the data model for an activity in the RaceDayMVC  for events
    public class ActivityViewModel
    {
        public int EventId { get; set; }

        [Required]

        [DisplayName("Event Name")]
        public string EventName { get; set; } = string.Empty;

        [DisplayName("Description")]

        public string? EventDescription { get; set; }

        [DisplayName("Event Date")]


        [DataType(DataType.DateTime)]
        public DateTime? EventDate { get; set; }

        [Required]

        public decimal Distance { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]

        [DisplayName("Event Type")]
        public string
         EventType { get; set; } = string.Empty;



    }
}
