namespace EconoHome.Domain.Entities
{
    // Entidade que representa uma pessoa no sistema
    // Pode ser adulto ou menor de idade (isso importa nas regras de transação)
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public int Age { get; private set; }
        
        // Lista de transações dessa pessoa (virtual pra funcionar o lazy loading do EF)
        public virtual ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        // Construtor protegido pro EF Core
        protected Person() { }

        public Person(string name, int age)
        {
            Id = Guid.NewGuid();
            Update(name, age);
        }

        // Método pra atualizar os dados da pessoa
        public void Update(string name, int age)
        {
            // Nome é obrigatório e tem limite de 200 caracteres
            if (string.IsNullOrWhiteSpace(name) || name.Length > 200)
                throw new ArgumentException("O nome é obrigatório e deve ter no máximo 200 caracteres.");

            // Não aceita idade negativa (seria meio impossível, né? 😅)
            if (age < 0) throw new ArgumentException("A idade não pode ser negativa.");

            Name = name;
            Age = age;
        }

        // Método helper pra saber se a pessoa é menor de idade
        // Isso é usado nas validações de transação porque menor não pode ter receita
        public bool IsUnderage() => Age < 18;
    }
}