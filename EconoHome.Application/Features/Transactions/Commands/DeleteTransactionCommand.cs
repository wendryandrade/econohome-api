using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoHome.Application.Features.Transactions.Commands
{
    public record DeleteTransactionCommand(Guid Id) : IRequest<Unit>;
}
