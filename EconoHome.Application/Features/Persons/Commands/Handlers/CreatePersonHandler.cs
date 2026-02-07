using EconoHome.Application.Features.Persons.Commands;
using EconoHome.Application.Interfaces;
using EconoHome.Domain.Entities;
using MediatR;

namespace EconoHome.Application.Features.Persons.Commands.Handlers
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly IPersonRepository _personRepository;

        public CreatePersonHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            // Validação manual estilo Ecommerce
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Nome é obrigatório.");

            var person = new Person(request.Name, request.Age);

            await _personRepository.AddAsync(person);
            // O SaveChangesAsync já está dentro do seu AddAsync no Repositório

            return person.Id;
        }
    }
}