using SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities;

namespace SistemaReuniao.Application.Abstractions;

    public interface IEmailService
    {
        Task EnviarEmailDeConfirmacaoDeEnvioDeConvite(Membro membro, Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao reuniao);

        Task EnviarEmailDeConfirmacaoDeAceiteDeConvite(Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao reuniao);
    }
