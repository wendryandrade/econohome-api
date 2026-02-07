using EconoHome.Application.Features.Transactions.DTOs;
using MediatR;

namespace EconoHome.Application.Features.Transactions.Queries
{
    public record GetAllTransactionsQuery() : IRequest<IEnumerable<TransactionResponse>>;
}
