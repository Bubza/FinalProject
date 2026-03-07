using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour?> GetByIdAsync(int id);
        Task CreateAsync(Tour tour);
        Task UpdateAsync(Tour tour);
        Task DeleteAsync(int id);
        Task<IEnumerable<Tour>> SearchAsync(string? search, int? destinationId, decimal? maxPrice);
        Task<IEnumerable<Tour>> GetRecommendationsAsync(int tourId, string? userId, int count = 3);
    }
}