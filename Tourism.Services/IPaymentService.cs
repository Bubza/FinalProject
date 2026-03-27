using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByBookingIdAsync(int bookingId);
        Task CreateAsync(Payment payment);
    }
}