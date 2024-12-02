using SistemaReuniao.Domain.Contexts.Membro.Errors;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;

namespace SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects
{
    public sealed record Nome
    {
        private Nome(string primeiroNome, string sobrenome)
        {
            if (string.IsNullOrWhiteSpace(primeiroNome))
                throw new ValidacaoException(DomainErrors.MEMBRO_PRIMEIRONOME_EMBRANCO);

            if (string.IsNullOrWhiteSpace(sobrenome))
                throw new ValidacaoException(DomainErrors.MEMBRO_SOBRENOME_EMBRANCO);

            PrimeiroNome = primeiroNome;
            Sobrenome = sobrenome;
        }

        public string PrimeiroNome { get; init; }

        public string Sobrenome { get; init; }

        public static Nome Criar(string primeiroNome, string sobrenome) => new Nome(primeiroNome, sobrenome);
    }
}
