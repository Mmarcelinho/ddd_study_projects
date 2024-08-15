using MediatR;
using SistemaReuniao.Application.Abstractions;       
using SistemaReuniao.Domain.Contexts.Membro.Repositories; 
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums; 
using SistemaReuniao.Domain.Contexts.Reuniao.Repositories; 
using SistemaReuniao.Domain.Repositories;                

namespace SistemaReuniao.Application.Convite.Commands.AceitarConvite;

// Comando que encapsula a intenção de aceitar um convite, identificado por seu ID
public sealed record AceitarConviteCommand(Guid ConviteId) : IRequest<Unit>;

// Handler responsável por processar o comando AceitarConviteCommand
public class AceitarConviteCommandHandler(
    IConviteRepositorio _conviteRepositorio,              // Repositório de convites
    IMembroRepositorio _membroRepositorio,                // Repositório de membros
    IReuniaoRepositorio _reuniaoRepositorio,              // Repositório de reuniões
    IParticipanteRepositorio _participanteRepositorio,    // Repositório de participantes
    IUnitOfWork _unitOfWork,                              // Unidade de trabalho para controle transacional
    IEmailService _emailService                           // Serviço de envio de emails
) : IRequestHandler<AceitarConviteCommand, Unit>          // Implementa IRequestHandler para processar o comando
{
    public async Task<Unit> Handle(AceitarConviteCommand request, CancellationToken cancellationToken)
    {
        // Recupera o convite pelo ID informado no comando
        var convite = await _conviteRepositorio.RecuperarPorIdAsync(request.ConviteId);

        // Verifica se o convite é nulo ou se seu status não é "Pendente". Se sim, encerra a operação
        if(convite is null || convite.Status != EStatusConvite.Pendente)
            return Unit.Value;

        // Recupera o membro associado ao convite
        var membro = await _membroRepositorio.RecuperarPorIdAsync(convite.MembroId);

        // Recupera a reunião associada ao convite, incluindo o criador da reunião
        var reuniao = await _reuniaoRepositorio.RecuperarPorIdComCriadorAsync(convite.ReuniaoId);

        // Se o membro ou a reunião forem nulos, encerra a operação
        if(membro is null || reuniao is null)
            return Unit.Value;

        // Aceita o convite e retorna o participante criado e uma mensagem associada
        var (participante, mensagem) = reuniao.AceitarConvite(convite);

        // Se o participante foi criado (não nulo), adiciona-o ao repositório de participantes
        if(participante is not null)
            _participanteRepositorio.Adicionar(participante);

        // Salva as mudanças na unidade de trabalho, persistindo as operações no banco de dados
        await _unitOfWork.SaveChangesAsync();

        // Se o status do convite for "Aceito", envia um email de confirmação de aceite
        if(convite.Status == EStatusConvite.Aceito)
            await _emailService.EnviarEmailDeConfirmacaoDeAceiteDeConvite(reuniao);

        // Retorna Unit.Value para sinalizar a conclusão do comando
        return Unit.Value;
    }
}
