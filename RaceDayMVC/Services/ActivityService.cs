using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public class ActivityService : ActivityInterface //updated ActvityService to handle Crud operations 
    {
        private readonly HttpClient _httpClient;

        public ActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient; // initializing the HttpClient instance to make HTTP requests to the API
        }

        public async Task<List<ActivityViewModel>> GetAllAsync() // This method retrieves all events asynchronously from the API

        {
            var response = await _httpClient.GetAsync("events");

            if (!response.IsSuccessStatusCode)
            {
                return new List<ActivityViewModel>();
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


            var events = JsonSerializer.Deserialize<List<ActivityViewModel>>(json, options); // dEserializes the JSON response into a list of ActivityViewModel objects

            return events ?? new List<ActivityViewModel>();
        }

        public async Task<ActivityViewModel?> GetByIdAsync(int id, string token) // gets specific events based on ID
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("events/" + id);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync(); // Reads the response content as a string
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<ActivityViewModel>(json, options); 
        }

        public async Task<bool> CreateAsync(ActivityViewModel model, string token) //Method to create a new event asynchronously by sending a POST request to the API
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("events", content);

            return response.IsSuccessStatusCode;
                  }

        public async Task<bool> UpdateAsync(int id, ActivityViewModel model, string token) //Edit/Update an existing event asynchronously by sending a PUT request to the API
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model);

             var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("events/" + id, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id, string token)//Delete an existing event asynchronously by sending a DELETE request to the API
        {
                 _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _httpClient.DeleteAsync("events/" + id);



            return response.IsSuccessStatusCode;
                   }
                         }
}