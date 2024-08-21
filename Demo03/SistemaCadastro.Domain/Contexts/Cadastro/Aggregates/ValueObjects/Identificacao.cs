using SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.Enums;

namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public record Identificacao
{
    private Identificacao(int? empresa, string identificador, EIdentificacaoTipo tipo)
    {
        Empresa = empresa;
        Identificador = identificador;
        Tipo = tipo;
    }

    public int? Empresa { get; init; }

    public string Identificador { get; init; }

    public EIdentificacaoTipo Tipo { get; init; }

    public static Identificacao Criar(int? empresa, string identificador, EIdentificacaoTipo tipo) => new(empresa, identificador, tipo);
}