using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public class CategoryServices // CategoryServices class provides methods to interact with the category API endpoints for CRUD operations on categories in the RaceDayMVC application
    {

        private readonly HttpClient _httpClient;

        public CategoryServices(HttpClient httpClient)
         {
            _httpClient = httpClient;// initializing the HttpClient instance to make HTTP requests to the API
        }


        public async Task<List<CategoryViewModelcs>> GetAllAsync(string token)
        { // gets all categories from the api

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("category"); //Async GET request to the "category" endpoint of the API to retrieve all categories

            if (!response.IsSuccessStatusCode)
            {
                return new List<CategoryViewModelcs>();
            }


            var json = await response.Content.ReadAsStringAsync(); // Reads the response content as a string

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var categories = JsonSerializer.Deserialize<List<CategoryViewModelcs>>(json, options);

            return categories ?? new List<CategoryViewModelcs>();

        }


        public async Task<CategoryViewModelcs?> GetByIdAsync(int id, string token) //Async method to retrieve a specific category by its ID from the API, using the provided JWT token for authorization
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("category/" + id);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<CategoryViewModelcs>(json, options);
              }


        public async Task<bool> CreateAsync(CategoryViewModelcs model, string token)
        { // sends new category to api synchronously by sending a POST request to the API with the provided JWT token for authorization
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("category", content);


            return response.IsSuccessStatusCode;
        }

        public async Task<string?> UpdateAsync(int id, CategoryViewModelcs model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(model); // Serialize the model object into a JSON string to be sent in the request body


            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("category/" + id, content);

            if (response.IsSuccessStatusCode) //if the update is successful, return null to indicate no error message
           {
                return null;
             }
              {
                return null;
            }

            var errorDetails = await response.Content.ReadAsStringAsync();

            return "Status " + (int)response.StatusCode + ": " + errorDetails;

         }


        public async Task<bool> DeleteAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync("category/" + id);

            return response.IsSuccessStatusCode; // Delete an existing category asynchronously by sending a DELETE request to the API and returning true if the deletion was successful  otherwise false
        }


                                   }

}