using EconoHome.Application.Features.Categories.DTOs;
using MediatR;

namespace EconoHome.Application.Features.Categories.Queries
{
    public record GetCategoriesSummaryQuery() : IRequest<CategoriesWithTotalSummaryResponse>;
}
