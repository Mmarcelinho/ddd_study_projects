using FluentAssertions;
using SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;
using SistemaReuniao.Domain.Contexts.Membro.Errors;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;

namespace Domain.Test.Membro;

public class EmailDomainTest
{
    [Fact]
    public void Construtor_Deve_Criar_Email_Quando_Valor_For_Valido()
    {
        // Arrange
        var emailValido = "teste@dominio.com";

        // Act
        var email = Email.Criar(emailValido);

        // Assert
        email.Valor.Should().Be(emailValido);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Construtor_Deve_Lançar_Excecao_Quando_Email_For_Nulo_Ou_Vazio(string email)
    {
        // Act
        Action action = () => Email.Criar(email);

        // Assert
        action.Should().Throw<ValidacaoException>()
            .WithMessage(DomainErrors.MEMBRO_EMAIL_EMBRANCO);
    }

    [Theory]
    [InlineData("emailinvalido")]
    public void Construtor_Deve_Lançar_Excecao_Quando_Email_For_Invalido(string email)
    {
        // Act
        Action action = () => Email.Criar(email);

        // Assert
        action.Should().Throw<ValidacaoException>()
            .WithMessage(DomainErrors.MEMBRO_EMAIL_INVALIDO);
    }
}
