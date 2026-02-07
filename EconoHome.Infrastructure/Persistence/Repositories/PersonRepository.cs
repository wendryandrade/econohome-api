using EconoHome.Application.Interfaces;
using EconoHome.Domain.Entities;
using EconoHome.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EconoHome.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly EconoHomeDbContext _context;

        public PersonRepository(EconoHomeDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.Persons
                .Include(p => p.Transactions)
                .AsTracking() 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            // O .Include é o que garante que as transações venham do banco junto com a pessoa
            return await _context.Persons
                .Include(p => p.Transactions)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            // Garante que a entidade principal está marcada como modificada
            _context.Entry(person).State = EntityState.Modified;

            // Carrega as transações atuais do banco para esta pessoa
            var existingTransactions = await _context.Transactions
                .Where(t => t.PersonId == person.Id)
                .ToListAsync();

            // Identifica o que foi removido da lista da entidade
            var removedTransactions = existingTransactions
                .Where(dbTrans => !person.Transactions.Any(t => t.Id == dbTrans.Id))
                .ToList();

            if (removedTransactions.Any())
            {
                _context.Transactions.RemoveRange(removedTransactions);
            }

            // Itera para Adicionar novas ou Modificar existentes
            foreach (var trans in person.Transactions)
            {
                if (trans.Id == Guid.Empty || !existingTransactions.Any(t => t.Id == trans.Id))
                {
                    _context.Entry(trans).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(trans).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                // Como foi configurado Cascade no DbContext, as transações serão removidas automaticamente pelo banco.
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}