using EconoHome.Domain.Enums;

namespace EconoHome.Domain.Entities
{
    // Categoria de transação (ex: "Alimentação", "Salário", etc)
    // Pode ser de despesa, receita, ou ambos
    public class Category
    {
        public Guid Id { get; set; }
        public string Description { get; private set; } = string.Empty;
        public CategoryPurpose Purpose { get; private set; }

        // Construtor protegido pro EF Core
        protected Category() { }

        public Category(string description, CategoryPurpose purpose)
        {
            Id = Guid.NewGuid();
            
            // Descrição é obrigatória e tem limite de 400 caracteres
            if (string.IsNullOrWhiteSpace(description) || description.Length > 400)
                throw new ArgumentException("A descrição é obrigatória (máx 400 caracteres).");

            Description = description;
            Purpose = purpose;
        }

        public void Update(string description, CategoryPurpose purpose)
        {
            // Mesma validação de quando cria
            if (string.IsNullOrWhiteSpace(description) || description.Length > 400)
                throw new ArgumentException("A descrição é obrigatória (máx 400 caracteres).");

            Description = description;
            Purpose = purpose;
        }

        // Método que verifica se essa categoria pode ser usada com determinado tipo de transação
        // Por exemplo:
        // - Categoria "Salário" (Income) NÃO pode ser usada numa despesa
        // - Categoria "Alimentação" (Expense) NÃO pode ser usada numa receita
        // - Categoria "Reembolsos" (Both) pode ser usada em qualquer uma
        public bool IsCompatibleWith(TransactionType type)
        {
            // Se é "Both", aceita qualquer coisa
            // Senão, tem que ser do mesmo tipo (Income com Income, Expense com Expense)
            return Purpose == CategoryPurpose.Both || (int)Purpose == (int)type;
        }
    }
}