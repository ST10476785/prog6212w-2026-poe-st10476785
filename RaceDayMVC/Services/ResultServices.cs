using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    // handles HTTP requests between the MVC app and the Results API endpoints
    public class ResultServices

    {
        private readonly HttpClient _httpClient; // initializing the HttpClient instance to make HTTP requests to the API


        public ResultServices(HttpClient httpClient)

        {
            _httpClient = httpClient;
        }


        public async Task<List<ResultViewModelcs>> GetAllAsync(string token) // an asynchronous method that retrieves all results from the API using the provided JWT token for authorization
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _httpClient.GetAsync("results");


            if (!response.IsSuccessStatusCode)

            {
                return new List<ResultViewModelcs>();


            }

       
            var json = await response.Content.ReadAsStringAsync(); //reads the response content as a string


            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };



            var results = JsonSerializer.Deserialize<List<ResultViewModelcs>>(json, options);




            return results ?? new List<ResultViewModelcs>();
        }

        // fetches a single result by its ID for editing
        public async Task<ResultViewModelcs?> GetByIdAsync(int id, string token) // an asynchronous method that retrieves a specific result by its ID from the API using the provided JWT token for authorization


        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



            var response = await _httpClient.GetAsync("results/" + id); // sends an asynchronous GET request to the "results/{id}" endpoint of the API to retrieve the result with the specified ID

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();




            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };



            return JsonSerializer.Deserialize<ResultViewModelcs>(json, options);





         }


        public async Task<string?> CreateAsync(ResultViewModelcs model, string token) // an asynchronous method that creates a new result by sending a POST request to the API with the provided JWT token for authorization, and returns an error message if the creation fails
            {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("results", content);

            if (response.IsSuccessStatusCode)
             {
                return null;
            }

            var errorDetails = await response.Content.ReadAsStringAsync();


            return "Status " + (int)response.StatusCode + ": " + errorDetails;


         }

        // sends updated result data to the API but is requried by the admin to update the result data, if the update fails an error message is returned
        public async Task<string?> UpdateAsync(int id, ResultViewModelcs model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model);
            
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await _httpClient.PutAsync("results/" + id, content);

            if (response.IsSuccessStatusCode)
            {
                return null;
                   }

            var errorDetails = await response.Content.ReadAsStringAsync();


            
            
            
            return "Status " + (int)response.StatusCode + ": " + errorDetails;


        }
        }
}