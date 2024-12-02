namespace SistemaEstoque.Domain.Errors;

public static class DomainErrors
{
    public const string NOME_PRODUTO_VAZIO = "Nome do produto é obrigatório.";

    public const string QUANTIDADE_NEGATIVO = "Quantidade não pode ser negativa.";

    public const string PESO_PRODUTO_NEGATIVO = "Peso não pode ser negativo.";

    public const string QUANTIDADE_ESTOQUE_INSUFICIENTE = "Quantidade em estoque insuficiente.";

    public const string NOME_CATEGORIA_VAZIO = "Nome da categoria é obrigatório.";

    public const string ENDERECO_RUA_VAZIO = "Rua do endereço é obrigatório.";

    public const string ENDERECO_NUMERO_VAZIO = "Número do endereço é obrigatório.";

    public const string ENDERECO_BAIRRO_VAZIO = "Bairro do endereço é obrigatório.";

    public const string ENDERECO_CIDADE_VAZIO = "Cidade do endereço é obrigatório.";

    public const string ENDERECO_ESTADO_VAZIO = "Estado do endereço é obrigatório.";

    public const string ENDERECO_CEP_VAZIO = "CEP do endereço é obrigatório.";

    public const string CNPJ_INVALIDO = "CNPJ inválido.";

    public const string FORNECEDOR_CONTATO_VAZIO = "Contato do fornecedor é obrigatório.";

    public const string TELEFONE_VAZIO = "O número de telefone não pode ser vazio.";

    public const string TELEFONE_FORMATO_INVALIDO = "O número de telefone está em um formato inválido. Use o formato: (XX) XXXXX-XXXX ou (XX) XXXX-XXXX.";

    public const string DATA_ENTREGA_MENOR_PEDIDO = "Data de entrega não pode ser anterior à data do pedido.";

    public const string TOTAL_NEGATIVO = "Total não pode ser negativo.";

    public const string VALOR_UNITARIO_NEGATIVO = "Valor unitário não pode ser negativo.";

    public const string LOTE_VAZIO = "Lote é obrigatório.";

    public const string FRETE_NEGATIVO = "Frete não pode ser negativo.";

    public const string IMPOSTO_NEGATIVO = "Imposto não pode ser negativo.";

    public const string INSCRICAO_ESTADUAL_VAZIA = "Inscrição estadual é obrigatória.";
}