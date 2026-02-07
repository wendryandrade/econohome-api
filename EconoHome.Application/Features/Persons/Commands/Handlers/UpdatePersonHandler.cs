using EconoHome.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoHome.Application.Features.Persons.Commands.Handlers
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, Unit>
    {
        private readonly IPersonRepository _repo;
        public UpdatePersonHandler(IPersonRepository repo) => _repo = repo;

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken ct)
        {
            var person = await _repo.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            // Atualiza os dados (certifique-se de que a entidade Person tenha esse método)
            person.Update(request.Name, request.Age);

            await _repo.UpdateAsync(person);
            return Unit.Value;
        }
    }
}
