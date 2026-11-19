using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public interface ActivityInterface
    {

        Task<List<ActivityViewModel>> GetAllAsync(); // Event interface method to retrieve all events asynchronously which i learnt from Code A Future (2025)
    }  
}
