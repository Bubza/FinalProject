using Microsoft.EntityFrameworkCore;
using Tourism.Data.Models.Entities;
using Tourism.Data;

namespace Tourism.Services
{
    public class TourOperatorService : ITourOperatorService
    {
        private readonly ApplicationDbContext _context;

        public TourOperatorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourOperator>> GetAllAsync()
        {
            return await _context.TourOperators.Include(o => o.Tours).ToListAsync();
        }

        public async Task<TourOperator?> GetByIdAsync(int id)
        {
            return await _context.TourOperators
                .Include(o => o.Tours).ThenInclude(t => t.Destination)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task CreateAsync(TourOperator tourOperator)
        {
            tourOperator.CreatedAt = DateTime.UtcNow;
            _context.TourOperators.Add(tourOperator);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TourOperator tourOperator)
        {
            _context.TourOperators.Update(tourOperator);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var op = await _context.TourOperators.FindAsync(id);
            if (op != null)
            {
                _context.TourOperators.Remove(op);
                await _context.SaveChangesAsync();
            }
        }
    }
}
