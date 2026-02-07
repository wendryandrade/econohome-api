namespace EconoHome.Application.Features.Categories.DTOs
{
    public record CategoriesWithTotalSummaryResponse(
        IEnumerable<CategorySummaryResponse> Categories,
        CategoryTotalSummary GeneralTotal);

    public record CategoryTotalSummary(
        decimal TotalIncomes,
        decimal TotalExpenses,
        decimal Balance);
}
