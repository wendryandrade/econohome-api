using EconoHome.Application.Features.Persons.DTOs;
using EconoHome.Application.Interfaces;
using MediatR;

namespace EconoHome.Application.Features.Persons.Queries.Handlers
{
    // Handler que busca todas as pessoas cadastradas
    // Basicamente pega tudo do banco e transforma em uma lista bonitinha pro front consumir
    public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonResponse>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllPersonsHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonResponse>> Handle(GetAllPersonsQuery request, CancellationToken ct)
        {
            // Busca todas as pessoas do banco
            var persons = await _personRepository.GetAllAsync();

            // Converte pra DTO porque o front não precisa saber de tudo que tem na entidade
            // Só manda o essencial: ID, nome e idade
            return persons.Select(p => new PersonResponse(p.Id, p.Name, p.Age));
        }
    }
}