using EconoHome.Application.Interfaces;
using EconoHome.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoHome.Application.Features.Transactions.Commands.Handlers
{
    // Handler pra editar uma transação que já existe
    // Aqui também rola as mesmas validações do Create, mas agora é pra atualizar
    public class UpdateTransactionHandler : IRequestHandler<UpdateTransactionCommand, Unit>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateTransactionHandler(
            ITransactionRepository transactionRepository,
            IPersonRepository personRepository,
            ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _personRepository = personRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken ct)
        {
            // Primeiro busca a transação que vai ser editada
            var transaction = await _transactionRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException("Transação não encontrada.");

            // Verifica se a pessoa existe
            var person = await _personRepository.GetByIdAsync(request.PersonId)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            // Verifica se a categoria existe
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId)
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            // Validação importante: Menor de idade não pode ter receita!
            // Se a pessoa tem menos de 18 e tá tentando botar receita, já era
            if (person.Age < 18 && request.Type == TransactionType.Income)
            {
                throw new InvalidOperationException("Menores de 18 anos não podem registrar receitas.");
            }

            // Outra validação: A categoria tem que fazer sentido com o tipo
            // Exemplo: não dá pra usar categoria "Salário" (que é Income) numa Despesa
            // A não ser que seja categoria "Both" que aceita os dois
            if (!category.IsCompatibleWith(request.Type))
            {
                throw new InvalidOperationException("O tipo da transação é incompatível com a categoria selecionada.");
            }

            // Tudo OK? Atualiza os dados
            transaction.Update(request.Description, request.Amount, request.Type, request.CategoryId, request.PersonId);

            await _transactionRepository.UpdateAsync(transaction);
            return Unit.Value;
        }
    }
}
