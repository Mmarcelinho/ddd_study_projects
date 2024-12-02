namespace SistemaCadastro.Domain.Contexts.Cadastro.Aggregates.ValueObjects;

public sealed record Telefone
{
    private Telefone(long numero, bool celular, bool whatsapp, bool telegram)
    {
        Numero = numero;
        Celular = celular;
        Whatsapp = whatsapp;
        Telegram = telegram;
    }

    public long Numero { get; init; }

    public bool Celular { get; init; }

    public bool Whatsapp { get; init; }

    public bool Telegram { get; init; }

    public static Telefone Criar(long numero, bool celular, bool whatsapp, bool telegram) =>
    new(numero, celular, whatsapp, telegram);
}