using SistemaEstoque.Domain.Errors;
using SistemaEstoque.Domain.Exceptions;

namespace SistemaEstoque.Domain.ValueObjects;

public sealed record Endereco
{
    public string Rua { get; private set; }
    public string Numero { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string Cep { get; private set; }

    private Endereco(
        string rua,
        string numero,
        string bairro,
        string cidade,
        string estado,
        string cep)
    {
        Rua = rua;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;
    }

    public static Endereco Criar(
        string rua,
        string numero,
        string bairro,
        string cidade,
        string estado,
        string cep)
    {
        ValidarCampo(rua, DomainErrors.ENDERECO_RUA_VAZIO);
        ValidarCampo(numero, DomainErrors.ENDERECO_NUMERO_VAZIO);
        ValidarCampo(bairro, DomainErrors.ENDERECO_BAIRRO_VAZIO);
        ValidarCampo(cidade, DomainErrors.ENDERECO_CIDADE_VAZIO);
        ValidarCampo(estado, DomainErrors.ENDERECO_ESTADO_VAZIO);
        ValidarCampo(cep, DomainErrors.ENDERECO_CEP_VAZIO);
        return new(rua, numero, bairro, cidade, estado, cep);
    }

    private static void ValidarCampo(string valor, string mensagemErro)
    {
        if (string.IsNullOrWhiteSpace(valor)) throw new DomainException(mensagemErro);
    }
}
