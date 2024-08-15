namespace SistemaReuniao.Domain.Contexts.Membro.Errors;

    public static class DomainErrors
    {
        public const string MEMBRO_PRIMEIRONOME_EMBRANCO = "O primeiro nome deve ser informado.";

        public const string MEMBRO_SOBRENOME_EMBRANCO = "O sobrenome deve ser informado.";

        public const string MEMBRO_EMAIL_EMBRANCO = "O e-mail deve ser informado.";

        public const string MEMBRO_EMAIL_INVALIDO = "Formato de e-mail inválido";
    }
