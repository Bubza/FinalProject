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
            var existing = await _context.Tours.FindAsync(tour.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(tour);
            }
            else
            {
                _context.Tours.Update(tour);
            }
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
                query = query.Where(t => t.Title.Contains(search)
                    || t.Destination.Name.Contains(search)
                    || t.Destination.Country.Contains(search));

            if (destinationId.HasValue)
                query = query.Where(t => t.DestinationId == destinationId);

            if (maxPrice.HasValue)
                query = query.Where(t => t.PricePerPerson <= maxPrice);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Returns personalised recommendations for a given tour page.
        /// Logic (in priority order):
        /// 1. If user is logged in — tours from destinations they have previously booked
        /// 2. Tours from the same destination as the current tour
        /// 3. Tours from the same operator
        /// 4. Top-rated tours as fallback
        /// Current tour is always excluded. Results are deduplicated.
        /// </summary>
        public async Task<IEnumerable<Tour>> GetRecommendationsAsync(int tourId, string? userId, int count = 3)
        {
            var currentTour = await _context.Tours
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == tourId);

            if (currentTour == null) return Enumerable.Empty<Tour>();

            var allTours = await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.TourOperator)
                .Include(t => t.Reviews)
                .Where(t => t.Id != tourId)
                .AsNoTracking()
                .ToListAsync();

            var result = new List<Tour>();

            // 1. Personalised: destinations the user has already booked
            if (!string.IsNullOrEmpty(userId))
            {
                var bookedDestinationIds = await _context.Bookings
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Tour)
                    .Select(b => b.Tour.DestinationId)
                    .Distinct()
                    .ToListAsync();

                if (bookedDestinationIds.Any())
                {
                    var personalised = allTours
                        .Where(t => bookedDestinationIds.Contains(t.DestinationId))
                        .OrderByDescending(t => t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0)
                        .Take(count)
                        .ToList();

                    result.AddRange(personalised);
                }
            }

            // 2. Same destination
            if (result.Count < count)
            {
                var sameDestination = allTours
                    .Where(t => t.DestinationId == currentTour.DestinationId && !result.Any(r => r.Id == t.Id))
                    .OrderByDescending(t => t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0)
                    .Take(count - result.Count)
                    .ToList();

                result.AddRange(sameDestination);
            }

            // 3. Same operator
            if (result.Count < count)
            {
                var sameOperator = allTours
                    .Where(t => t.TourOperatorId == currentTour.TourOperatorId && !result.Any(r => r.Id == t.Id))
                    .OrderByDescending(t => t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0)
                    .Take(count - result.Count)
                    .ToList();

                result.AddRange(sameOperator);
            }

            // 4. Fallback: top rated
            if (result.Count < count)
            {
                var topRated = allTours
                    .Where(t => !result.Any(r => r.Id == t.Id))
                    .OrderByDescending(t => t.Reviews.Any() ? t.Reviews.Average(r => r.Rating) : 0)
                    .Take(count - result.Count)
                    .ToList();

                result.AddRange(topRated);
            }

            return result.Take(count);
        }
    }
}