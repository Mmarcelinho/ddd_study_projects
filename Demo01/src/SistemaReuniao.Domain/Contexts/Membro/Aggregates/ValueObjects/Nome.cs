using SistemaReuniao.Domain.Contexts.Membro.Errors;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;

namespace SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects
{
    public sealed record Nome
    {
        // Construtor privado que garante a criação de instâncias de Nome apenas através do método de fábrica
        // e somente após validação dos parâmetros.
        private Nome(string primeiroNome, string sobrenome)
        {
            // Validação para garantir que o primeiro nome não é nulo ou vazio.
            if(string.IsNullOrWhiteSpace(primeiroNome))
                throw new ValidacaoException(DomainErrors.MEMBRO_PRIMEIRONOME_EMBRANCO);
            
            // Validação para garantir que o sobrenome não é nulo ou vazio.
            if(string.IsNullOrWhiteSpace(sobrenome))
                throw new ValidacaoException(DomainErrors.MEMBRO_SOBRENOME_EMBRANCO);

            // Se as validações passarem, os valores são atribuídos às propriedades imutáveis.
            PrimeiroNome = primeiroNome;
            Sobrenome = sobrenome;
        }

        // Propriedades imutáveis que armazenam o primeiro nome e o sobrenome.
        public string PrimeiroNome { get; init; }

        public string Sobrenome { get; init; }

        // Método de fábrica que encapsula a criação de uma nova instância de Nome,
        // garantindo que a criação passe pelas validações definidas no construtor.
        public static Nome Criar(string primeiroNome, string sobrenome) => new Nome(primeiroNome, sobrenome);
    }
}
