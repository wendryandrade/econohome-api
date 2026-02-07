using MediatR;

namespace EconoHome.Application.Features.Categories.Commands
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;
}