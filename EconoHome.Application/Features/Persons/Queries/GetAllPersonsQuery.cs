using EconoHome.Application.Features.Persons.DTOs;
using MediatR;

namespace EconoHome.Application.Features.Persons.Queries
{
    public record GetAllPersonsQuery() : IRequest<IEnumerable<PersonResponse>>;
}