using EconoHome.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoHome.Application.Features.Transactions.Commands
{
    public record UpdateTransactionCommand(
        Guid Id,
        string Description,
        decimal Amount,
        TransactionType Type,
        Guid PersonId,
        Guid CategoryId
    ) : IRequest<Unit>;
}

