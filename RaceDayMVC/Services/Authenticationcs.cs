using System.Text.Json;
using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public class Authenticationcs : AuthenticationInterface
    { //Authentication Services class to handle user authentication and registration by sending HTTP requests to the API endpoints for login and registration, and processing the responses accordingly
        private readonly HttpClient _httpClient;

        public Authenticationcs(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(LoginViewModel loginData)
        {
            var  response = await _httpClient.PostAsJsonAsync("auth/login", loginData);

            
            if (!response.IsSuccessStatusCode)
            { // If not successful  return null to indicate that the login attempt failed
                return null;


            }

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            if (json.TrimStart().StartsWith("{"))
            {
                var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);// Deserialize the JSON response into a dictionary to extract the token value
                if (data != null && data.ContainsKey("token"))
                {

                    return data["token"];
                }
                return null;
            }

            return json.Trim().Trim('"');
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel registerData)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", registerData);

            if (response.IsSuccessStatusCode) // If registration is successful prompt this response and for the user to login
            {
                
                
                return (true, "You have been successfully registered, you may log in with your newly made account");
            }

            var errorMessage = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(errorMessage))
            { // if registration fails prompt this error message to the user to try again


                errorMessage = "User could not be registered, please try again";
            }

            return (false, errorMessage);
               }
    }
}
          