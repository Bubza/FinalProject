using Microsoft.EntityFrameworkCore;
using Tourism.Data.Models.Entities;
using Tourism.Data;

namespace Tourism.Services
{
    public class TourService : ITourService
    {
        private readonly ApplicationDbContext _context;

        public TourService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tour>> GetAllAsync()
        {
            return await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
                .Include(t => t.Reviews)
                .ToListAsync();
        }

        public async Task<Tour?> GetByIdAsync(int id)
        {
            return await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
                .Include(t => t.Reviews)
                .Include(t => t.Bookings)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateAsync(Tour tour)
        {
            tour.CreatedAt = DateTime.UtcNow;
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tour tour)
        {
            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tour>> SearchAsync(string? search, int? destinationId, decimal? maxPrice)
        {
            var query = _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
                .Include(t => t.Reviews)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Title.Contains(search) || t.Destination.Name.Contains(search));

            if (destinationId.HasValue)
                query = query.Where(t => t.DestinationId == destinationId);

            if (maxPrice.HasValue)
                query = query.Where(t => t.PricePerPerson <= maxPrice);

            return await query.ToListAsync();
        }
    }
}
