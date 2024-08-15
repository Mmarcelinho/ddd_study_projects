using SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;
using SistemaReuniao.Domain.Contexts.Membro.Primitives;

namespace SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities;

public sealed class Membro : Entidade
{
    // O construtor é privado, seguindo o padrão de fábrica. Isso impede que o Membro seja criado diretamente
    // por outros contextos ou camadas, garantindo que a criação de uma instância de Membro passe por uma 
    // verificação de regras de negócio dentro do próprio domínio.
    private Membro(Guid id, Nome nome, Email email) : base(id)
    {
        Nome = nome;
        Email = email;
    }

    // Propriedades imutáveis para Nome e Email, ambas encapsuladas em Value Objects, 
    // garantindo que as regras de negócio sejam respeitadas e promovendo a imutabilidade e consistência do domínio.
    public Nome Nome { get; private set; }

    public Email Email { get; private set; }

    // Método estático Criar para instanciar um novo Membro, agindo como um factory method.
    // Ele encapsula a lógica de criação e atribui um novo Guid ao Membro, reforçando a regra de que
    // um Membro deve ser criado com uma identidade única.
    public static Membro Criar(Nome nome, Email email) => new(Guid.NewGuid(), nome, email);
}
