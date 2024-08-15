namespace SistemaReuniao.Domain.Contexts.Reuniao.Errors;

    public static class DomainErrors
    {
        public const string REUNIAO_NUMERO_MAXIMO_PARTICIPANTES = "O número máximo de participantes não pode ser nulo.";

        public const string REUNIAO_VALIDADE_DO_CONVITE = "A validade do convite não pode ser nulo.";

        public const string REUNIAO_DATA_EXPIRACAO_MENOR_ATUAL = "A data de expiração dos convites não pode ser anterior ao momento atual.";

        public const string REUNIAO_CONVIDAR_CRIADOR = "Não é possível enviar convite para o criador da reunião.";

        public const string REUNIAO_JA_REALIZADA = "Não é possível enviar convite para reunião no passado.";

        public const string REUNIAO_EXPIRADA = "Não é possível aceitar o convite para uma reunião expirada.";
    }
