using EconoHome.Domain.Enums;
using MediatR;

namespace EconoHome.Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(Guid Id, string Description, CategoryPurpose Purpose) : IRequest<Unit>;
}