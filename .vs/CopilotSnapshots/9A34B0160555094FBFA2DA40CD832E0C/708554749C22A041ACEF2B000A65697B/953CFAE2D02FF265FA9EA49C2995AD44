using EconoHome.Application.Interfaces;
using MediatR;

namespace EconoHome.Application.Features.Categories.Commands.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryHandler(ICategoryRepository repo) => _categoryRepository = repo;

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken ct)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            category.Update(request.Description, request.Purpose);

            await _categoryRepository.UpdateAsync(category);
            return Unit.Value;
        }
    }
}
