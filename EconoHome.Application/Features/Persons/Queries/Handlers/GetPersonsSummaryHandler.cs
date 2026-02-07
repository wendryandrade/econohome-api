using EconoHome.Application.Features.Persons.DTOs;
using EconoHome.Application.Interfaces;
using EconoHome.Domain.Enums;
using MediatR;

namespace EconoHome.Application.Features.Persons.Queries.Handlers
{

    public class GetPersonsSummaryHandler : IRequestHandler<GetPersonsSummaryQuery, PersonsWithTotalSummaryResponse>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonsSummaryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonsWithTotalSummaryResponse> Handle(GetPersonsSummaryQuery request, CancellationToken ct)
        {
            var persons = await _personRepository.GetAllAsync();

            var personsSummary = persons.Select(p =>
            {
                var incomes = p.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                var expenses = p.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

                return new PersonSummaryResponse(
                    p.Id,
                    p.Name,
                    incomes,
                    expenses,
                    incomes - expenses
                );
            }).ToList();

            var totalIncomes = personsSummary.Sum(p => p.TotalIncomes);
            var totalExpenses = personsSummary.Sum(p => p.TotalExpenses);
            var totalBalance = totalIncomes - totalExpenses;

            var generalTotal = new TotalSummary(totalIncomes, totalExpenses, totalBalance);

            return new PersonsWithTotalSummaryResponse(personsSummary, generalTotal);
        }
    }
}