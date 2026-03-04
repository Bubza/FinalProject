using Microsoft.EntityFrameworkCore;
using Tourism.Data;
using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public class FavoriteTourService : IFavoriteTourService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteTourService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FavoriteTour>> GetByUserIdAsync(string userId)
        {
            return await _context.FavoriteTours
                .Include(f => f.Tour)
                    .ThenInclude(t => t.Destination)
                .Include(f => f.Tour)
                    .ThenInclude(t => t.TourOperator)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.AddedAt)
                .ToListAsync();
        }

        public async Task<bool> IsFavoriteAsync(string userId, int tourId)
        {
            return await _context.FavoriteTours
                .AnyAsync(f => f.UserId == userId && f.TourId == tourId);
        }

        public async Task AddAsync(string userId, int tourId)
        {
            if (await IsFavoriteAsync(userId, tourId)) return;

            var favorite = new FavoriteTour
            {
                UserId = userId,
                TourId = tourId
            };

            _context.FavoriteTours.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(string userId, int tourId)
        {
            var favorite = await _context.FavoriteTours
                .FirstOrDefaultAsync(f => f.UserId == userId && f.TourId == tourId);

            if (favorite != null)
            {
                _context.FavoriteTours.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }
    }
}
