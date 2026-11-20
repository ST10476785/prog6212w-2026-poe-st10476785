using System.ComponentModel.DataAnnotations;

namespace RaceDayMVC.Models
{
    public class AuthViewModel
    {
               }
//AuthViewModel class was created as BOTH authentication and registration both require details from the User and UserType View Models which is combined in this class

    public class LoginViewModel // Login setup requiring email and password for authentication only
           {
        [Required, EmailAddress]

        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]



        public string Password { get; set; } = string.Empty;
          }

    public class RegisterViewModel // Register setup requiring email, password, name, surname and usertype for registration only
         {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Surname { get; set; } = string.Empty;

        [Required, EmailAddress]


        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]


        public string Password { get; set; } = string.Empty;

        [Required]




        [Display(Name = "User Type")]
        public int UserTypeId { get; set; } = 2; // HOW ROLES WORK ROLE 1 = ADMIN// role2= USER/PARTICIPANT
           }




}
