namespace SistemaReuniao.Domain.Contexts.Reuniao.Primitives;

public abstract class Entidade : IEquatable<Entidade>
{
    // Construtor protegido que inicializa o Id da entidade. Esse construtor é acessível apenas dentro da classe base
    // e nas classes derivadas, garantindo que o Id seja atribuído de forma controlada.
    protected Entidade(Guid id)
    {
        Id = id;
    }

    // Propriedade que representa a identidade única da entidade. O Id é imutável após a criação,
    // o que garante que a identidade da entidade não será alterada.
    public Guid Id { get; private set; }

    // Implementação do operador de igualdade (==) para comparar duas entidades. O operador verifica se ambas as entidades
    // são diferentes de nulo e se são iguais, com base na comparação de seus Ids.
    public static bool operator ==(Entidade? first, Entidade? second) 
        => first is not null && second is not null && first.Equals(second);

    // Implementação do operador de desigualdade (!=) para comparar duas entidades. O operador retorna verdadeiro se as
    // entidades não forem iguais, com base na comparação de seus Ids.
    public static bool operator !=(Entidade? first, Entidade? second) 
        => !(first == second);

    // Implementação do método Equals para comparar duas instâncias de Entidade. 
    // Primeiro, verifica se o objeto comparado é nulo e se é do mesmo tipo. Se ambos os testes passarem, compara
    // os Ids das entidades.
    public bool Equals(Entidade? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;
    }

    // Sobrescrita do método Equals para comparar uma entidade com um objeto genérico. 
    // Primeiro, verifica se o objeto é nulo e se é uma instância de Entidade. Se for, compara os Ids das entidades.
    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj is not Entidade entidade)
            return false;

        return entidade.Id == Id;
    }

    // Sobrescrita do método GetHashCode para garantir que duas entidades iguais (com o mesmo Id) tenham o mesmo código
    // de hash. Isso é importante para a correta utilização das entidades em coleções que dependem de hashing, como dicionários.
    public override int GetHashCode() 
        => Id.GetHashCode() * 41; // Multiplicação por 41 é uma prática comum para minimizar colisões de hash.
}
