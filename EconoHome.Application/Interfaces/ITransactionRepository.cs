using EconoHome.Domain.Entities;

namespace EconoHome.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Guid id);
    }
}