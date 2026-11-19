
using System.ComponentModel.DataAnnotations;
namespace RaceDayMVC.Models { 
    

    public class UserViewModel
    {
        [Required] //User viewmodel class based on the original User entity without replicated business logic


        public int UserId { get; set; } 
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Surname { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int UserTypeId { get; set; }
    }
    
    }

