using EconoHome.Application.Interfaces;
using MediatR;

namespace EconoHome.Application.Features.Persons.Commands.Handlers
{
    public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken ct)
        {
            var person = await _personRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            await _personRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
