using EconoHome.Application.Features.Persons.DTOs;
using EconoHome.Application.Interfaces;
using MediatR;

namespace EconoHome.Application.Features.Persons.Queries.Handlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonResponse>
    {
        private readonly IPersonRepository _personRepository;

        public GetPersonByIdHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonResponse> Handle(GetPersonByIdQuery request, CancellationToken ct)
        {
            var person = await _personRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            return new PersonResponse(person.Id, person.Name, person.Age);
        }
    }
}
