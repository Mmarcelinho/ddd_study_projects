using MediatR;
using SistemaReuniao.Domain.Contexts.Membro.Repositories;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Repositories;
using SistemaReuniao.Domain.Repositories;

namespace SistemaReuniao.Application.Reuniao.Commands.CriarReuniao;

// O comando representa a intenção de criar uma nova reunião no sistema
public sealed record CriarReuniaoCommand(
    Guid MembroId,                          // Identificador do membro que está criando a reunião
    ETipoReuniao Tipo,                      // Tipo da reunião (por exemplo, com número fixo de participantes ou expiração de convites)
    DateTime AgendadoEmUtc,                 // Data e hora em que a reunião está agendada (UTC)
    string Nome,                            // Nome da reunião
    string? Local,                          // Local onde a reunião será realizada (opcional)
    int? NumeroMaximoDeParticipantes,       // Número máximo de participantes permitidos (opcional)
    int? ValidadeDosConvitesEmHoras         // Validade dos convites em horas (opcional)
) : IRequest<Unit>;                         // O comando implementa IRequest<Unit> do MediatR, indicando que ele não retorna dados, apenas sinaliza a conclusão da operação

// O handler é responsável por lidar com o comando CriarReuniaoCommand
public class CriarReuniaoCommandHandler(
    IMembroRepositorio _membroRepositorio,  // Dependência do repositório de membros, usada para acessar dados de membros
    IReuniaoRepositorio _reuniaoRepositorio, // Dependência do repositório de reuniões, usada para acessar e persistir dados de reuniões
    IUnitOfWork _unitOfWork                  // Unidade de trabalho que agrupa operações de banco de dados, garantindo consistência transacional
) : IRequestHandler<CriarReuniaoCommand, Unit> // O handler implementa IRequestHandler, indicando que ele processa o comando CriarReuniaoCommand e retorna Unit
{
    // Método Handle processa o comando recebido
    public async Task<Unit> Handle(CriarReuniaoCommand request, CancellationToken cancellationToken)
    {
        // Recupera o membro que está criando a reunião
        var membro = await _membroRepositorio.RecuperarPorIdAsync(request.MembroId);

        // Valida se o membro existe; se não, retorna Unit.Value, indicando que o comando foi processado sem realizar a operação
        if (membro is null)
            return Unit.Value;

        // Cria a nova entidade Reuniao utilizando o método de fábrica na agregada Reuniao
        var reuniao = Domain.Contexts.Reuniao.Aggregates.Entities.Reuniao.Criar(
            Guid.NewGuid(),                     // Gera um novo identificador único para a reunião
            membro,                             // Membro que está criando a reunião
            request.Tipo,                       // Tipo da reunião
            request.AgendadoEmUtc,              // Data e hora agendada para a reunião
            request.Nome,                       // Nome da reunião
            request.Local,                      // Local da reunião (se aplicável)
            request.NumeroMaximoDeParticipantes, // Número máximo de participantes (se aplicável)
            request.ValidadeDosConvitesEmHoras  // Validade dos convites em horas (se aplicável)
        );

        // Adiciona a nova reunião ao repositório de reuniões
        _reuniaoRepositorio.Adicionar(reuniao);

        // Salva as mudanças na unidade de trabalho, garantindo que a operação seja persistida no banco de dados
        await _unitOfWork.SaveChangesAsync();

        // Retorna Unit.Value, indicando que o comando foi processado com sucesso, sem necessidade de retorno de valor
        return Unit.Value;
    }
}
