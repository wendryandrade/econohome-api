using EconoHome.Application.Features.Categories.Commands;
using EconoHome.Application.Interfaces;
using MediatR;

namespace EconoHome.Application.Features.Categories.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken ct)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            // O repositório vai cuidar da remoção no banco
            await _categoryRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}