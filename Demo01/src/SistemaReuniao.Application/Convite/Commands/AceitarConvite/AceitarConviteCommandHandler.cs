using MediatR;
using SistemaReuniao.Application.Abstractions;
using SistemaReuniao.Domain.Contexts.Membro.Repositories;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Repositories;
using SistemaReuniao.Domain.Repositories;

namespace SistemaReuniao.Application.Convite.Commands.AceitarConvite;

public sealed record AceitarConviteCommand(Guid ConviteId) : IRequest<Unit>;

public class AceitarConviteCommandHandler(
    IConviteRepositorio _conviteRepositorio,
    IMembroRepositorio _membroRepositorio,
    IReuniaoRepositorio _reuniaoRepositorio,
    IParticipanteRepositorio _participanteRepositorio,
    IUnitOfWork _unitOfWork,
    IEmailService _emailService
) : IRequestHandler<AceitarConviteCommand, Unit>
{
    public async Task<Unit> Handle(AceitarConviteCommand request, CancellationToken cancellationToken)
    {
        var convite = await _conviteRepositorio.RecuperarPorIdAsync(request.ConviteId);

        if (convite is null || convite.Status != EStatusConvite.Pendente)
            return Unit.Value;

        var membro = await _membroRepositorio.RecuperarPorIdAsync(convite.MembroId);

        var reuniao = await _reuniaoRepositorio.RecuperarPorIdComCriadorAsync(convite.ReuniaoId);

        if (membro is null || reuniao is null)
            return Unit.Value;

        var (participante, mensagem) = reuniao.AceitarConvite(convite);

        if (participante is not null)
            _participanteRepositorio.Adicionar(participante);

        await _unitOfWork.SaveChangesAsync();

        if (convite.Status == EStatusConvite.Aceito)
            await _emailService.EnviarEmailDeConfirmacaoDeAceiteDeConvite(reuniao);

        return Unit.Value;
    }
}
