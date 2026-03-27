using Microsoft.EntityFrameworkCore;
using Tourism.Data;
using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories.Include(c => c.Tours).ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _context.Categories.Include(c => c.Tours).FirstOrDefaultAsync(c => c.Id == id);
    }
}