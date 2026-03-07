using Tourism.Data.Models.Entities;
using Tourism.Data;
namespace Tourism.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<IEnumerable<Booking>> GetByUserIdAsync(string userId);
        Task<Booking?> GetByIdAsync(int id);
        Task CreateAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
        Task<int> GetBookedSpotsAsync(int tourId);
    }
}