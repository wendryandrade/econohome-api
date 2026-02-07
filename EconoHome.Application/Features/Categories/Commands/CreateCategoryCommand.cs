using EconoHome.Domain.Enums;
using MediatR;

namespace EconoHome.Application.Features.Categories.Commands
{
    public record CreateCategoryCommand(string Description, CategoryPurpose Purpose) : IRequest<Guid>;
}