using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public interface IFavoriteTourService
    {
        Task<IEnumerable<FavoriteTour>> GetByUserIdAsync(string userId);
        Task<bool> IsFavoriteAsync(string userId, int tourId);
        Task AddAsync(string userId, int tourId);
        Task RemoveAsync(string userId, int tourId);
    }
}