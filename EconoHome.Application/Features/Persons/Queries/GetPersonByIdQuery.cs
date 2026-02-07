using EconoHome.Application.Features.Persons.DTOs;
using MediatR;

namespace EconoHome.Application.Features.Persons.Queries
{
    public record GetPersonByIdQuery(Guid Id) : IRequest<PersonResponse>;
}
