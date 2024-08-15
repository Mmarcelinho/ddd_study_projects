using FluentAssertions;
using SistemaReuniao.Domain.Contexts.Membro.Aggregates.ValueObjects;
using SistemaReuniao.Domain.Contexts.Membro.Errors;
using SistemaReuniao.Domain.Contexts.Membro.Exceptions;

namespace Domain.Test.Membro;

    public class NomeDomainTest
    {
        [Fact]
        public void Construtor_Deve_Criar_Nome_Quando_Valores_São_Validos()
        {
            // Arrange
            var primeiroNome = "PrimeiroNome";
            var sobrenome = "Sobrenome";

            // Act
            var nome = Nome.Criar(primeiroNome, sobrenome);

            // Assert
            nome.PrimeiroNome.Should().Be(primeiroNome);
            nome.Sobrenome.Should().Be(sobrenome);
        }

        [Theory]
        [InlineData(null, "Sobrenome")]
        [InlineData("", "Sobrenome")]
        public void Construtor_Deve_Lançar_Excecao_Quando_Nome_For_Nulo_Ou_Vazio(string primeiroNome, string sobrenome)
        {
            // Act
            Action action = () => Nome.Criar(primeiroNome, sobrenome);

            // Assert
            action.Should().Throw<ValidacaoException>()
            .WithMessage(DomainErrors.MEMBRO_PRIMEIRONOME_EMBRANCO);
        }

        [Theory]
        [InlineData("primeiroNome", null)]
        [InlineData("primeiroNome", "")]
        public void Construtor_Deve_Lançar_Excecao_Quando_Sobrenome_For_Nulo_Ou_Vazio(string primeiroNome, string sobrenome)
        {
            // Act
            Action action = () => Nome.Criar(primeiroNome, sobrenome);

            // Assert
            action.Should().Throw<ValidacaoException>()
            .WithMessage(DomainErrors.MEMBRO_SOBRENOME_EMBRANCO);
        }
        
    }
