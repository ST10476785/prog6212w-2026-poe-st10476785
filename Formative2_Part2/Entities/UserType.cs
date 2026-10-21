using Microsoft.EntityFrameworkCore;

namespace Formative2_Part2.Entities
{
    public class UserType // UserType class to represent different types of users in the system based on Part1's planning
    {                 // Required properties represent not null values from part 1
        public int UserTypeId { get; set; }
        public required string TypeName { get; set; }
        public string TypeDescription { get; set; }


    }

    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }
        public int UserTypeId { get; set; } // Foreign key to UserType

        public UserType UserType { get; set; } // Foreign key relationship to UserType




    }

    public class Event
    { // Event class to represent events in the system based on Part1's planning
        public int EventId { get; set; }
        public required string EventName { get; set; }

        public string ?EventDescription { get; set; }

        public DateTime? EventDate { get; set; }

        public decimal Distance { get; set; }

        public required string Location { get; set; }

        public required string EventType { get; set; }

      


    }

    public class Category
    {

        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }

        public string ? CategoryDescription { get; set; }

        public int EventId { get; set; } // Foreign key to Event

        public Event Event { get; set; } // Foreign key relationship to Event


    }

    public class EventEnrollment
    {  //EventEnrollment class to represent the enrollment of users in events based on Part1's planning
        public int EventEnrollmentId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.Now; // Default value for EnrollmentDate AS the current date
        
        public required string Status { get; set; } 

        public int UserId { get; set; } // Foreign key to User

        public User User { get; set; } // Foreign key relationship to User

        public int CategoryId { get; set; } // Foreign key to Category

        public Category Category { get; set; } // Foreign key relationship to Category









    }

    public class Result {

        public int ResultId { get; set; }
         public TimeOnly FinishingTime { get; set; } // TimeOnly property to represent the finishing time of a user in an event

        public int FinishingPosition { get; set; } 

        public DateTime ResultDate { get; set; } = DateTime.Now; 

        public int EventEnrollmentId { get; set; } // Foreign key to EventEnrollment

        public EventEnrollment EventEnrollment { get; set; } // Foreign key relationship to EventEnrollment







    }
        
    }








