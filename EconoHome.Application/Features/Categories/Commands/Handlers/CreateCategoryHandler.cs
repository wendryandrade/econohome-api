using EconoHome.Application.Features.Categories.Commands;
using EconoHome.Application.Interfaces;
using EconoHome.Domain.Entities;
using MediatR;

namespace EconoHome.Application.Features.Categories.Handlers
{

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Description, request.Purpose);

            await _categoryRepository.AddAsync(category);
            return category.Id;
        }
    }
}