using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public class EventEnrollmentService // EventEnrollmentService class is defined to handle the business logic for managing event enrollments in the RaceDayMVC application, including retrieving, creating, and deleting enrollments through HTTP requests to an API
    {
        private readonly HttpClient _httpClient;

        public EventEnrollmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<EventEnrollmentViewModel>> GetAllAsync(string token)//An asynchronous method that retrieves all event enrollments from the API using the provided JWT token for authorization
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("enrollment");

            if (!response.IsSuccessStatusCode)
            {
                return new List<EventEnrollmentViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };



            var enrollments = JsonSerializer.Deserialize<List<EventEnrollmentViewModel>>(json, options);

            return enrollments ?? new List<EventEnrollmentViewModel>();


        }


        public async Task<string?> CreateAsync(EventEnrollmentViewModel model, string token) // an asynchronous method that creates a new event enrollment by sending a POST request to the API with the provided JWT token for authorization, and returns an error message if the creation fails
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model);


            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("enrollment", content);


            if (response.IsSuccessStatusCode)

            {
                return null;
            }

            var errorDetails = await response.Content.ReadAsStringAsync();



            return "Status " + (int)response.StatusCode + ": " + errorDetails;
        }


        public async Task<bool> DeleteAsync(int id, string token) // an asynchronous method that deletes an event enrollment by its ID by sending a DELETE request to the API with the provided Jwt token for authorization  and returns a boolean indicating whether the deletion was successful
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await _httpClient.DeleteAsync("enrollment/" + id);


                return response.IsSuccessStatusCode;

            }
        }
      }
}