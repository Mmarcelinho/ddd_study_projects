using MediatR;
using SistemaReuniao.Domain.Contexts.Membro.Repositories;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Repositories;
using SistemaReuniao.Domain.Repositories;

namespace SistemaReuniao.Application.Reuniao.Commands.CriarReuniao;

public sealed record CriarReuniaoCommand(
    Guid MembroId,
    ETipoReuniao Tipo,
    DateTime AgendadoEmUtc,
    string Nome,
    string? Local,
    int? NumeroMaximoDeParticipantes,
    int? ValidadeDosConvitesEmHoras
) : IRequest<Unit>;

public class CriarReuniaoCommandHandler(
    IMembroRepositorio _membroRepositorio,
    IReuniaoRepositorio _reuniaoRepositorio,
    IUnitOfWork _unitOfWork
) : IRequestHandler<CriarReuniaoCommand, Unit>
{
    public async Task<Unit> Handle(CriarReuniaoCommand request, CancellationToken cancellationToken)
    {
        var membro = await _membroRepositorio.RecuperarPorIdAsync(request.MembroId);

        if (membro is null)
            return Unit.Value;

        var reuniao = Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao.Criar(
            Guid.NewGuid(),
            membro,
            request.Tipo,
            request.AgendadoEmUtc,
            request.Nome,
            request.Local,
            request.NumeroMaximoDeParticipantes,
            request.ValidadeDosConvitesEmHoras
        );

        _reuniaoRepositorio.Adicionar(reuniao);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
