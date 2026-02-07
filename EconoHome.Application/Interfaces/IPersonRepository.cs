using EconoHome.Domain.Entities;

namespace EconoHome.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(Guid id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(Guid id);
    }
}