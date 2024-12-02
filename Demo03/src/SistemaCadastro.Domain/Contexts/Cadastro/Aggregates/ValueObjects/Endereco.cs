namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public sealed record Endereco
{
    private Endereco(
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string complemento,
        string pontoReferencia,
        string uf,
        string cidade,
        int ibge)
    {
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Complemento = complemento;
        PontoReferencia = pontoReferencia;
        Uf = uf;
        Cidade = cidade;
        Ibge = ibge;
    }

    public string Cep { get; init; }

    public string Logradouro { get; init; }

    public string Numero { get; init; }

    public string Bairro { get; init; }

    public string Complemento { get; init; }

    public string PontoReferencia { get; init; }

    public string Uf { get; init; }

    public string Cidade { get; init; }

    public int Ibge { get; init; }

    public static Endereco Criar(
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string complemento,
        string pontoReferencia,
        string uf,
        string cidade,
        int ibge) =>
        new(
            cep,
            logradouro,
            numero,
            bairro,
            complemento,
            pontoReferencia,
            uf,
            cidade,
            ibge
        );
}
