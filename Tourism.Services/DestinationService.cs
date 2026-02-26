using Microsoft.EntityFrameworkCore;
using Tourism.Data.Models.Entities;
using Tourism.Data;

namespace Tourism.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly ApplicationDbContext _context;

        public DestinationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _context.Destinations.Include(d => d.Tours).ToListAsync();
        }

        public async Task<Destination?> GetByIdAsync(int id)
        {
            return await _context.Destinations.Include(d => d.Tours).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task CreateAsync(Destination destination)
        {
            destination.Tours ??= new List<Tour>();
            destination.Description ??= string.Empty;
            destination.ImageUrl ??= string.Empty;
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destination destination)
        {
            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination != null)
            {
                _context.Destinations.Remove(destination);
                await _context.SaveChangesAsync();
            }
        }
    }
}