using Microsoft.EntityFrameworkCore;
using Tourism.Data;
using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly ApplicationDbContext _context;
        public ContactMessageService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<ContactMessage>> GetAllAsync() =>
            await _context.ContactMessages.OrderByDescending(m => m.SentAt).ToListAsync();

        public async Task CreateAsync(ContactMessage message)
        {
            message.SentAt = DateTime.UtcNow;
            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var msg = await _context.ContactMessages.FindAsync(id);
            if (msg != null) { msg.IsRead = true; await _context.SaveChangesAsync(); }
        }

        public async Task DeleteAsync(int id)
        {
            var msg = await _context.ContactMessages.FindAsync(id);
            if (msg != null) { _context.ContactMessages.Remove(msg); await _context.SaveChangesAsync(); }
        }
    }
}