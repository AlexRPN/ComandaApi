
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.GruposAdicionais.Entidades
{
    public class GruposAdicionaisTestes
    {
        private readonly GrupoAdicional sut;
        public GruposAdicionaisTestes()
        {
            sut = Builder<GrupoAdicional>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new GrupoAdicionalComando
                {
                    EmpresaId = 1,
                    Nome = "Grupo Adicional teste"
                };

                var grupoAdicional = new GrupoAdicional(comando);

                Assert.Equal(comando.EmpresaId, grupoAdicional.EmpresaId);
                Assert.Equal(comando.Nome, grupoAdicional.Nome);
            }
        }

        public class SetEmpresaId : GruposAdicionaisTestes
        {
            [Fact]
            public void Quando_EmpresaIdForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaEmpresaId = 42;
                // Act
                sut.SetEmpresaId(novaEmpresaId);
                // Assert
                Assert.Equal(novaEmpresaId, sut.EmpresaId);
            }
        }

        public class SetNome : GruposAdicionaisTestes
        {
            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var adicional = "Adicional teste";

                // Act
                sut.SetNome(adicional);

                // Assert
                Assert.Equal(adicional, sut.Nome);
            }

            [Fact]
            public void Quando_NomeForNulo_Espero_ArgumentNullException()
            {
                // Arrange
                string nomeAdicional = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(nomeAdicional));
                Assert.Equal("O Adicional não pode ser nulo ou vazio!", exception.Message);
            }

            [Fact]
            public void Quando_NomeForVazio_Espero_ArgumentNullException()
            {
                // Arrange
                string nomeAdicional = "";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(nomeAdicional));
                Assert.Equal("O Adicional não pode ser nulo ou vazio!", exception.Message);
            }
        }
    }
}
