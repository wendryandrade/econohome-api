using EconoHome.Application.Features.Categories.DTOs;
using EconoHome.Application.Interfaces;
using EconoHome.Domain.Enums;
using MediatR;

namespace EconoHome.Application.Features.Categories.Queries.Handlers
{
    public class GetCategoriesSummaryHandler : IRequestHandler<GetCategoriesSummaryQuery, CategoriesWithTotalSummaryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository;

        public GetCategoriesSummaryHandler(
            ICategoryRepository categoryRepository,
            ITransactionRepository transactionRepository)
        {
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<CategoriesWithTotalSummaryResponse> Handle(GetCategoriesSummaryQuery request, CancellationToken ct)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var allTransactions = await _transactionRepository.GetAllAsync();

            var categoriesSummary = categories.Select(c =>
            {
                var categoryTransactions = allTransactions.Where(t => t.CategoryId == c.Id);
                
                var incomes = categoryTransactions
                    .Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Amount);
                
                var expenses = categoryTransactions
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                return new CategorySummaryResponse(
                    c.Id,
                    c.Description,
                    c.Purpose.ToString(),
                    incomes,
                    expenses,
                    incomes - expenses
                );
            }).ToList();

            var totalIncomes = categoriesSummary.Sum(c => c.TotalIncomes);
            var totalExpenses = categoriesSummary.Sum(c => c.TotalExpenses);
            var totalBalance = totalIncomes - totalExpenses;

            var generalTotal = new CategoryTotalSummary(totalIncomes, totalExpenses, totalBalance);

            return new CategoriesWithTotalSummaryResponse(categoriesSummary, generalTotal);
        }
    }
}
