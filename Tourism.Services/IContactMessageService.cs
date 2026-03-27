using Tourism.Data.Models.Entities;

namespace Tourism.Services
{
    public interface IContactMessageService
    {
        Task<IEnumerable<ContactMessage>> GetAllAsync();
        Task CreateAsync(ContactMessage message);
        Task MarkAsReadAsync(int id);
        Task DeleteAsync(int id);
    }
}