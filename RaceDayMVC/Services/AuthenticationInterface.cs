using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public interface AuthenticationInterface
    {

        Task<string?> LoginAsync(LoginViewModel model); // Auth interface method to handle user login, using the  created LoginViewModel class to pass the login credentials and return a string token if successfull

        Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model);
    }  // If registration is successful return a tuple containing a  boolean value which indicates success, if unsuccessful return a tuple containing a boolean value which indicates a failure
}
