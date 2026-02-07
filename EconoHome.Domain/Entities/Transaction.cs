using EconoHome.Domain.Enums;

namespace EconoHome.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; } = null!;

        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; } = null!;

        // Construtor protegido pro EF Core conseguir criar a entidade
        protected Transaction() { }

        // Aqui é onde tudo acontece! Construtor que cria uma transação validando TUDO
        public Transaction(string description, decimal amount, TransactionType type, Person person, Category category)
        {
            // Não aceita valor zero ou negativo (seria estranho, né?)
            if (amount <= 0) throw new ArgumentException("O valor deve ser positivo.");
            
            // Descrição é obrigatória e não pode ter mais de 400 caracteres
            if (string.IsNullOrWhiteSpace(description) || description.Length > 400)
                throw new ArgumentException("Descrição obrigatória (máx 400 caracteres).");

            // REGRA IMPORTANTE: Menor de idade só pode ter despesa!
            // A lógica é simples: criança não trabalha, então não tem receita
            // IsUnderage() retorna true se a pessoa tem menos de 18 anos
            if (person.IsUnderage() && type == TransactionType.Income)
                throw new InvalidOperationException("Menores de 18 anos só podem registrar despesas.");

            // REGRA IMPORTANTE: A categoria precisa fazer sentido com o tipo
            // Por exemplo: não dá pra usar a categoria "Salário" numa despesa
            // O método IsCompatibleWith verifica se a categoria aceita esse tipo de transação
            if (!category.IsCompatibleWith(type))
                throw new InvalidOperationException("A categoria selecionada é incompatível com o tipo de transação.");

            // Tudo OK? Então vamos criar a transação!
            Id = Guid.NewGuid();
            Description = description;
            Amount = amount;
            Type = type;
            CreatedAt = DateTime.UtcNow;

            PersonId = person.Id;
            Person = person;
            CategoryId = category.Id;
            Category = category;
        }

        // Método pra atualizar os dados de uma transação existente
        public void Update(string description, decimal amount, TransactionType type, Guid categoryId, Guid personId)
        {
            // Mesmas validações básicas do construtor
            if (amount <= 0) throw new ArgumentException("O valor deve ser positivo.");
            
            if (string.IsNullOrWhiteSpace(description) || description.Length > 400)
                throw new ArgumentException("Descrição obrigatória (máx 400 caracteres).");

            Description = description;
            Amount = amount;
            Type = type;
            CategoryId = categoryId;
            PersonId = personId;
        }
    }
}