using EconoHome.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoHome.Application.Features.Transactions.Commands.Handlers
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, Unit>
    {
        private readonly ITransactionRepository _repo;
        public DeleteTransactionHandler(ITransactionRepository repo) => _repo = repo;

        public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken ct)
        {
            await _repo.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
