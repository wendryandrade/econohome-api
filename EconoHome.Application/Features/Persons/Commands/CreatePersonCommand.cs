using MediatR;

namespace EconoHome.Application.Features.Persons.Commands
{
    public record CreatePersonCommand(string Name, int Age) : IRequest<Guid>;
}