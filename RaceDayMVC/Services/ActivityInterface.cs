using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public interface ActivityInterface
    {

        Task<List<ActivityViewModel>> GetAllAsync(); // Event interface method to retrieve all events asynchronously which i learnt from Code A Future (2025)
        Task<ActivityViewModel?> GetByIdAsync(int id, string token); //Event interface method to retrieve a specific event asynchronously by its Id 
        Task<bool> CreateAsync(ActivityViewModel model, string token); //Event interface method to create a new event asynchronousl 
        Task<bool> UpdateAsync(int id, ActivityViewModel model, string token); //Event interface method to update an existing event asynchronously by its Id
        Task<bool> DeleteAsync(int id, string token);// Event interface method to delete an existing event asynchronously by its Id
    }
}