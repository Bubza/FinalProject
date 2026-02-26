using Tourism.Data.Models.Entities;
using Tourism.Data;

namespace Tourism.Services
{
    public interface ITourOperatorService
    {
        Task<IEnumerable<TourOperator>> GetAllAsync();
        Task<TourOperator?> GetByIdAsync(int id);
        Task CreateAsync(TourOperator tourOperator);
        Task UpdateAsync(TourOperator tourOperator);
        Task DeleteAsync(int id);
    }
}
