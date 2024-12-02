namespace SistemaEstoque.Domain.Validators;

public static class CNPJValidator
{
    private static readonly int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    public static bool IsCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) return false;

        cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14 || IsKnownInvalidCnpj(cnpj)) return false;

        var tempCnpj = cnpj[..12];
        var soma = 0;

        for (var i = 0; i < 12; i++)
        {
            if (!int.TryParse(tempCnpj[i].ToString(), out int digit)) return false;
            soma += digit * multiplicador1[i];
        }

        var resto = soma % 11;
        var primeiroDigito = resto < 2 ? 0 : 11 - resto;

        tempCnpj += primeiroDigito;
        soma = 0;

        for (var i = 0; i < 13; i++)
        {
            if (!int.TryParse(tempCnpj[i].ToString(), out int digit)) return false;
            soma += digit * multiplicador2[i];
        }

        resto = soma % 11;
        var segundoDigito = resto < 2 ? 0 : 11 - resto;

        var digitosVerificadores = $"{primeiroDigito}{segundoDigito}";

        return cnpj.EndsWith(digitosVerificadores);
    }

    private static bool IsKnownInvalidCnpj(string cnpj)
    {
        string[] invalidCnpjs = [
            "00000000000000", "11111111111111", "22222222222222",
            "33333333333333", "44444444444444", "55555555555555",
            "66666666666666", "77777777777777", "88888888888888",
            "99999999999999"
        ];
        return invalidCnpjs.Contains(cnpj);
    }
}
