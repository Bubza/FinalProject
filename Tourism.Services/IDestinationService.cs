using Tourism.Data.Models.Entities;
using Tourism.Data;
namespace Tourism.Services
{
    public interface IDestinationService
    {
        Task<IEnumerable<Destination>> GetAllAsync();
        Task<Destination?> GetByIdAsync(int id);
        Task CreateAsync(Destination destination);
        Task UpdateAsync(Destination destination);
        Task DeleteAsync(int id);
    }
}
