using EconoHome.Domain.Enums;
using MediatR;

namespace EconoHome.Application.Features.Transactions.Commands
{

    public record CreateTransactionCommand(
        string Description,
        decimal Amount,
        TransactionType Type,
        Guid PersonId,
        Guid CategoryId) : IRequest<Guid>;
}