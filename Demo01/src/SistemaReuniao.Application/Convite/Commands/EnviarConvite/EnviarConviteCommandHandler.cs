using MediatR;
using SistemaReuniao.Application.Abstractions;        
using SistemaReuniao.Domain.Contexts.Membro.Repositories;
using SistemaReuniao.Domain.Contexts.Reuniao.Repositories; 
using SistemaReuniao.Domain.Repositories;              

namespace SistemaReuniao.Application.Convite.Commands.EnviarConvite;

// O comando representa a intenção de enviar um convite para um membro em uma reunião específica
public sealed record EnviarConviteCommand(Guid MembroId, Guid ReuniaoId) : IRequest<Unit>; 

// O handler é responsável por processar o comando EnviarConviteCommand
public class EnviarConviteCommandHandler(
    IMembroRepositorio _membroRepositorio,          // Dependência do repositório de membros
    IReuniaoRepositorio _reuniaoRepositorio,        // Dependência do repositório de reuniões
    IConviteRepositorio _conviteRepositorio,        // Dependência do repositório de convites
    IUnitOfWork _unitOfWork,                        // Unidade de trabalho para consistência transacional
    IEmailService _emailService                     // Serviço de envio de emails para comunicação
) : IRequestHandler<EnviarConviteCommand, Unit>      // O handler implementa IRequestHandler para processar o comando
{
    // Método Handle processa o comando recebido
    public async Task<Unit> Handle(EnviarConviteCommand request, CancellationToken cancellationToken)
    {
        // Recupera o membro pelo ID informado no comando
        var membro = await _membroRepositorio.RecuperarPorIdAsync(request.MembroId);

        // Recupera a reunião pelo ID informado no comando, incluindo informações do criador da reunião
        var reuniao = await _reuniaoRepositorio.RecuperarPorIdComCriadorAsync(request.ReuniaoId);

        // Valida se o membro ou a reunião são nulos; se algum deles for, retorna Unit.Value sem realizar nenhuma ação
        if (membro is null || reuniao is null)
            return Unit.Value;

        // Envia o convite para o membro na reunião especificada
        var convite = reuniao.EnviarConvite(membro);

        // Adiciona o convite recém-criado ao repositório de convites
        _conviteRepositorio.Adicionar(convite);

        // Salva as alterações na unidade de trabalho, garantindo que as mudanças sejam persistidas no banco de dados
        await _unitOfWork.SaveChangesAsync();

        // Envia um email de confirmação ao membro para informar sobre o envio do convite
        await _emailService.EnviarEmailDeConfirmacaoDeEnvioDeConvite(membro, reuniao);

        // Retorna Unit.Value para sinalizar a conclusão do comando
        return Unit.Value;
    }
}
