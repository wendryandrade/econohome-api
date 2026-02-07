namespace EconoHome.Application.Features.Persons.DTOs
{
    public record PersonsWithTotalSummaryResponse(
        IEnumerable<PersonSummaryResponse> Persons,
        TotalSummary GeneralTotal);

    public record TotalSummary(
        decimal TotalIncomes,
        decimal TotalExpenses,
        decimal Balance);
}
