namespace EconoHome.Application.Features.Persons.DTOs
{
    public record PersonSummaryResponse(
        Guid PersonId,
        string Name,
        decimal TotalIncomes,
        decimal TotalExpenses,
        decimal Balance);
}