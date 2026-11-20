using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public class ActivityService : ActivityInterface // use the ActivityService class to implement the ActivityInterface interface 

    {
        private readonly HttpClient _client; // initialized  a private readonly HttpClient field to make HTTP requests to the API

        public ActivityService(HttpClient client)
        {
            _client = client;
         }

        public async Task<List<ActivityViewModel>> GetAllAsync()
        {
            var result = await _client.GetFromJsonAsync<List<ActivityViewModel>>("events");// "evemts endpoint" to retrieve a list of events from the API asynchronously using the GetFromJsonAsync metho
            return result ?? new List<ActivityViewModel>(); //list of events is returned,  or an empty list if the result is not listed
        }
    }
}
