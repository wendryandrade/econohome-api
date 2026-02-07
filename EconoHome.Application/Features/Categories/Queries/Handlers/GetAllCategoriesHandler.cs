using EconoHome.Application.Features.Categories.DTOs;
using EconoHome.Application.Interfaces;
using MediatR;

namespace EconoHome.Application.Features.Categories.Queries.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken ct)
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryResponse(
                c.Id,
                c.Description,
                c.Purpose.ToString() // Convertendo Enum para String para o DTO
            ));
        }
    }
}