using EconoHome.Application.Features.Transactions.Commands;
using EconoHome.Application.Interfaces;
using EconoHome.Domain.Entities;
using MediatR;

namespace EconoHome.Application.Features.Transactions.Handlers
{
    // Handler que cria uma nova transação (receita ou despesa)
    // Aqui é onde a gente valida as regras chatas tipo "menor de idade não pode ter receita"
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateTransactionHandler(ITransactionRepository tRepo, IPersonRepository pRepo, ICategoryRepository cRepo)
        {
            _transactionRepository = tRepo;
            _personRepository = pRepo;
            _categoryRepository = cRepo;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken ct)
        {
            // Primeiro verifica se a pessoa existe no banco
            var person = await _personRepository.GetByIdAsync(request.PersonId)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            // Depois verifica se a categoria existe
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            // Aqui acontece a mágica! O construtor da Transaction valida tudo:
            // 1. Se o valor é positivo (não pode ter transação negativa)
            // 2. Se a descrição tem no máximo 400 caracteres
            // 3. REGRA IMPORTANTE: Se a pessoa tem menos de 18 anos, não pode registrar receita
            //    (tipo, criança não tem salário, né? só gastam dinheiro dos pais 😅)
            // 4. REGRA IMPORTANTE: A categoria tem que bater com o tipo da transação
            //    (não dá pra usar categoria "Salário" numa despesa, faz sentido?)
            var transaction = new Transaction(request.Description, request.Amount, request.Type, person, category);

            // Salva no banco e retorna o ID gerado
            await _transactionRepository.AddAsync(transaction);
            return transaction.Id;
        }
    }
}