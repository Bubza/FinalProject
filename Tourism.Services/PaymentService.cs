using Microsoft.EntityFrameworkCore;
using Tourism.Data;
using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        public PaymentService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Payment>> GetAllAsync() =>
            await _context.Payments.Include(p => p.Booking).OrderByDescending(p => p.PaidAt).ToListAsync();

        public async Task<Payment?> GetByBookingIdAsync(int bookingId) =>
            await _context.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);

        public async Task CreateAsync(Payment payment)
        {
            payment.PaidAt = DateTime.UtcNow;
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}