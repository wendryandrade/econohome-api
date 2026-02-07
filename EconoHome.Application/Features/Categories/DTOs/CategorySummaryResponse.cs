namespace EconoHome.Application.Features.Categories.DTOs
{
    public record CategorySummaryResponse(
        Guid CategoryId,
        string Description,
        string Purpose,
        decimal TotalIncomes,
        decimal TotalExpenses,
        decimal Balance);
}
