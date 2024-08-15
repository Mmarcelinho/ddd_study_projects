using FluentAssertions;
using SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;

namespace Domain.Test.Membro;

public class MembroDomainTest
{
    [Fact]
    public void Criar_Deve_Retornar_Membro_Quando_Nome_E_Email_São_Validos()
    {
        // Arrange
        var nome = Nome.Criar("PrimeiroNome", "Sobrenome");
        var email = Email.Criar("teste@dominio.com");

        // Act
        var membro = SistemaReuniao.Domain.Contexts.Membro.Aggregates.Entities.Membro.Criar(nome, email);

        // Assert
        membro.Should().NotBeNull();
        membro.Nome.Should().Be(nome);
        membro.Email.Should().Be(email);
    }
}
