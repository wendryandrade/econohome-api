using MediatR;

namespace EconoHome.Application.Features.Persons.Commands
{
    public record DeletePersonCommand(Guid Id) : IRequest<Unit>;
}
