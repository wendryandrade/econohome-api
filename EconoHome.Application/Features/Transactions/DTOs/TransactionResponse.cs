using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoHome.Application.Features.Transactions.DTOs
{
    public record TransactionResponse(
        Guid Id,
        string Description,
        decimal Amount,
        int Type,
        string CategoryName,
        string PersonName
    );
}
