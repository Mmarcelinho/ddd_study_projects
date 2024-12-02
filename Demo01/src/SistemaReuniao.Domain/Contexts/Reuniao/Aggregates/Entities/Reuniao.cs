using SistemaReuniao.Domain.Contexts.Reuniao.Primitives;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Enums;
using SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.ValueObjects;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;
using SistemaReuniao.Domain.Contexts.Reuniao.Errors;

namespace SistemaReuniao.Domain.Contexts.Reuniao.Aggregates.Entities;

public sealed class Reuniao : Entidade
{
    private readonly List<Convite> _convites = [];
    private readonly List<Participante> _participantes = [];

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

    public Membro.Aggregates.Entities.Membro Criador { get; private set; }
    public ETipoReuniao Tipo { get; private set; }
    public string Nome { get; private set; }
    public DateTime AgendadoEmUtc { get; private set; }
    public string? Local { get; private set; }
    public int? NumeroMaximoDeParticipantes { get; private set; }
    public DateTime ConvitesExpiramEmUtc { get; private set; }
    public int NumeroDeParticipantes { get; private set; }

    public IReadOnlyCollection<Participante> Participantes => _participantes.AsReadOnly();
    public IReadOnlyCollection<Convite> Convites => _convites.AsReadOnly();

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
        var reuniao = new Reuniao(
            id,
            criador,
            tipo,
            agendadoEmUtc,
            nome,
            local
        );

        reuniao.CalcularDetalhesDoTipoDeReuniao(numeroMaximoDeParticipantes, validadeDosConvitesEmHoras);

        return reuniao;
    }

    private void CalcularDetalhesDoTipoDeReuniao(
    int? numeroMaximoDeParticipantes,
    int? validadeDosConvitesEmHoras)
    {
        switch (Tipo)
        {
            case ETipoReuniao.ComNumeroFixoDeParticipantes:
                if (numeroMaximoDeParticipantes is null)
                    throw new ValidacaoException(DomainErrors.REUNIAO_NUMERO_MAXIMO_PARTICIPANTES);

                NumeroMaximoDeParticipantes = numeroMaximoDeParticipantes;
                break;

            case ETipoReuniao.ComExpiracaoDeConvites:
                if (validadeDosConvitesEmHoras is null)
                    throw new ValidacaoException(DomainErrors.REUNIAO_VALIDADE_DO_CONVITE);

                var dataExpiracao = AgendadoEmUtc.AddHours(-validadeDosConvitesEmHoras.Value);

                if (dataExpiracao <= DateTime.UtcNow)
                    throw new ValidacaoException(DomainErrors.REUNIAO_DATA_EXPIRACAO_MENOR_ATUAL);

                ConvitesExpiramEmUtc = dataExpiracao;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(ETipoReuniao));
        }
    }

    public Convite EnviarConvite(Membro.Aggregates.Entities.Membro membro)
    {
        if (Criador.Id == membro.Id)
            throw new ValidacaoException(DomainErrors.REUNIAO_CONVIDAR_CRIADOR);

        if (AgendadoEmUtc < DateTime.UtcNow)
            throw new ValidacaoException(DomainErrors.REUNIAO_JA_REALIZADA);

        var convite = new Convite(Guid.NewGuid(), membro, this);
        _convites.Add(convite);

        return convite;
    }

    public (Participante?, string? mensagem) AceitarConvite(Convite convite)
    {
        var numeroMaximoDeParticipantesAlcancado = Tipo == ETipoReuniao.ComNumeroFixoDeParticipantes && NumeroDeParticipantes == NumeroMaximoDeParticipantes;

        var expiracaoDosConvitesAlcancada = Tipo == ETipoReuniao.ComExpiracaoDeConvites && ConvitesExpiramEmUtc < DateTime.UtcNow;

        var expirado = numeroMaximoDeParticipantesAlcancado || expiracaoDosConvitesAlcancada;

        if (expirado)
        {

            convite.Expirado();
            return (null, DomainErrors.REUNIAO_EXPIRADA);
        }

        var participante = convite.Aceito();
        _participantes.Add(participante);
        NumeroDeParticipantes++;

        return (participante, string.Empty);
    }
}
