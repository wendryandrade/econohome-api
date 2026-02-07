using EconoHome.Application.Interfaces;
using EconoHome.Domain.Entities;
using EconoHome.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EconoHome.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly EconoHomeDbContext _context;

        public TransactionRepository(EconoHomeDbContext context)
        {
            _context = context;
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.Person)
                .AsTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.Person)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            // É forçado o estado para garantir que o EF não tente inserir novamente uma Pessoa ou Categoria que já existe
            _context.Entry(transaction).State = EntityState.Added;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}