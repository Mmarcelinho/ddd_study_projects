using MediatR;
using SistemaReuniao.Application.Abstractions;
using SistemaReuniao.Domain.Contexts.Membro.Repositories;
using SistemaReuniao.Domain.Contexts.Reuniao.Repositories;
using SistemaReuniao.Domain.Repositories;

namespace SistemaReuniao.Application.Convite.Commands.EnviarConvite;

public sealed record EnviarConviteCommand(Guid MembroId, Guid ReuniaoId) : IRequest<Unit>;

public class EnviarConviteCommandHandler(
    IMembroRepositorio _membroRepositorio,
    IReuniaoRepositorio _reuniaoRepositorio,
    IConviteRepositorio _conviteRepositorio,
    IUnitOfWork _unitOfWork,
    IEmailService _emailService
) : IRequestHandler<EnviarConviteCommand, Unit>
{
    public async Task<Unit> Handle(EnviarConviteCommand request, CancellationToken cancellationToken)
    {
        var membro = await _membroRepositorio.RecuperarPorIdAsync(request.MembroId);

        var reuniao = await _reuniaoRepositorio.RecuperarPorIdComCriadorAsync(request.ReuniaoId);

        if (membro is null || reuniao is null)
            return Unit.Value;
        var convite = reuniao.EnviarConvite(membro);

        _conviteRepositorio.Adicionar(convite);

        await _unitOfWork.SaveChangesAsync();

        await _emailService.EnviarEmailDeConfirmacaoDeEnvioDeConvite(membro, reuniao);

        return Unit.Value;
    }
}
