
using System.ComponentModel.DataAnnotations;

namespace RaceDayMVC.Models // UserTypeViewModel class represents the data model for a user type in thE raceDayMVC application for user roles such as admin or Logged in user

{
    public class UserTypeViewModel
    {

        public int UserTypeId { get; set; }

        [Required]
        public string TypeName { get; set; } = string.Empty;

        public string? TypeDescription { get; set; }
    }
}
