using SistemaReuniao.Domain.Contexts.Reuniao.Primitives;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.ValueObjects;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;
using SistemaReuniao.Domain.Contexts.Reuniao.Errors;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities;

public sealed class Reuniao : Entidade
{
    // Listas privadas para convites e participantes, protegendo a integridade do agregado "Reuniao".
    private readonly List<Convite> _convites = new();
    private readonly List<Participante> _participantes = new();

    // Construtor privado assegura que a criação da reunião ocorra apenas através de métodos de fábrica.
    private Reuniao(
        Guid id,
        Membro.Aggregates.Entities.Membro criador,
        ETipoReuniao tipo,
        DateTime agendadoEmUtc,
        string nome,
        string? local) : base(id)
    {
        Criador = criador;
        Tipo = tipo;
        AgendadoEmUtc = agendadoEmUtc;
        Nome = nome;
        Local = local;
    }

    // Propriedades que descrevem o estado da entidade "Reuniao".
    public Membro.Aggregates.Entities.Membro Criador { get; private set; }
    public ETipoReuniao Tipo { get; private set; }
    public string Nome { get; private set; }
    public DateTime AgendadoEmUtc { get; private set; }
    public string? Local { get; private set; }
    public int? NumeroMaximoDeParticipantes { get; private set; }
    public DateTime ConvitesExpiramEmUtc { get; private set; }
    public int NumeroDeParticipantes { get; private set; }

    // Coleções somente leitura expostas para manter a integridade do agregado.
    public IReadOnlyCollection<Participante> Participantes => _participantes;
    public IReadOnlyCollection<Convite> Convites => _convites;

    // Método de fábrica para criar uma nova reunião, aplicando regras de negócio na criação.
    public static Reuniao Criar(
        Guid id,
        Membro.Aggregates.Entities.Membro criador,
        ETipoReuniao tipo,
        DateTime agendadoEmUtc,
        string nome,
        string? local,
        int? numeroMaximoDeParticipantes,
        int? validadeDosConvitesEmHoras)
    {
        // Instancia uma nova reunião.
        var reuniao = new Reuniao(
            id,
            criador,
            tipo,
            agendadoEmUtc,
            nome,
            local
        );

        // Calcula os detalhes específicos com base no tipo de reunião.
        reuniao.CalcularDetalhesDoTipoDeReuniao(numeroMaximoDeParticipantes, validadeDosConvitesEmHoras);

        // Retorna a nova instância da reunião.
        return reuniao;
    }

    // Método privado para calcular detalhes adicionais com base no tipo de reunião.
    private void CalcularDetalhesDoTipoDeReuniao(
    int? numeroMaximoDeParticipantes,
    int? validadeDosConvitesEmHoras)
    {
        // Diferentes tipos de reuniões exigem regras de negócio distintas.
        switch (Tipo)
        {
            case ETipoReuniao.ComNumeroFixoDeParticipantes:
                if (numeroMaximoDeParticipantes is null)
                    throw new ValidacaoException(DomainErrors.REUNIAO_NUMERO_MAXIMO_PARTICIPANTES);

                // Define o número máximo de participantes permitido.
                NumeroMaximoDeParticipantes = numeroMaximoDeParticipantes;
                break;

            case ETipoReuniao.ComExpiracaoDeConvites:
                if (validadeDosConvitesEmHoras is null)
                    throw new ValidacaoException(DomainErrors.REUNIAO_VALIDADE_DO_CONVITE);

                // Calcula a data de expiração dos convites.
                var dataExpiracao = AgendadoEmUtc.AddHours(-validadeDosConvitesEmHoras.Value);

                // Valida se a data de expiração não é anterior à data atual.
                if (dataExpiracao <= DateTime.UtcNow)
                    throw new ValidacaoException(DomainErrors.REUNIAO_DATA_EXPIRACAO_MENOR_ATUAL);

                ConvitesExpiramEmUtc = dataExpiracao;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(ETipoReuniao));
        }
    }

    // Método para enviar um convite, respeitando as regras de negócio.
    public Convite EnviarConvite(Membro.Aggregates.Entities.Membro membro)
    {
        // Verifica se o criador da reunião não pode se convidar.
        if (Criador.Id == membro.Id)
            throw new ValidacaoException(DomainErrors.REUNIAO_CONVIDAR_CRIADOR);

        // Verifica se a reunião já ocorreu.
        if (AgendadoEmUtc < DateTime.UtcNow)
            throw new ValidacaoException(DomainErrors.REUNIAO_JA_REALIZADA);

        // Cria um novo convite e o adiciona à coleção de convites.
        var convite = new Convite(Guid.NewGuid(), membro, this);
        _convites.Add(convite);

        return convite;
    }

    // Método para aceitar um convite, respeitando as regras de negócio.
    public (Participante?, string? mensagem) AceitarConvite(Convite convite)
    {
        // Verifica se o número máximo de participantes foi alcançado.
        var numeroMaximoDeParticipantesAlcancado = Tipo == ETipoReuniao.ComNumeroFixoDeParticipantes && NumeroDeParticipantes == NumeroMaximoDeParticipantes;

        // Verifica se os convites expiraram.
        var expiracaoDosConvitesAlcancada = Tipo == ETipoReuniao.ComExpiracaoDeConvites && ConvitesExpiramEmUtc < DateTime.UtcNow;

        // Determina se a reunião está expirada com base nas condições anteriores.
        var expirado = numeroMaximoDeParticipantesAlcancado || expiracaoDosConvitesAlcancada;

        if (expirado)
        {
            // Marca o convite como expirado e retorna uma mensagem de erro.
            convite.Expirado();
            return (null, DomainErrors.REUNIAO_EXPIRADA);
        }

        // Se ainda não expirado, aceita o convite e adiciona o participante à reunião.
        var participante = convite.Aceito();
        _participantes.Add(participante);
        NumeroDeParticipantes++;

        return (participante, string.Empty);
    }
}
