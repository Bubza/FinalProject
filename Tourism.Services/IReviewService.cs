using Tourism.Data.Models.Entities;
using Tourism.Data;
namespace Tourism.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<IEnumerable<Review>> GetByTourIdAsync(int tourId);
        Task<IEnumerable<Review>> GetByUserIdAsync(string userId);
        Task<Review?> GetByIdAsync(int id);
        Task CreateAsync(Review review);
        Task DeleteAsync(int id);
    }
}