
using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Adicionais.Entidades
{
    public class AdicionaisTestes
    {
        private readonly Adicional sut;
        public AdicionaisTestes()
        {
            sut = Builder<Adicional>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                // Arrange
                var comando = new AdicionalComando
                {
                    GrupoAdicionalId = 1,
                    Nome = "Adicional Teste",
                    Valor = 10.0m,
                    DataCadastro = DateTime.UtcNow,
                    DataAlteracao = DateTime.UtcNow,
                    Status = StatusEnum.Ativo
                };
                // Act
                var adicional = new Adicional(comando);
                // Assert
                Assert.Equal(comando.GrupoAdicionalId, adicional.GrupoAdicionalId);
                Assert.Equal(comando.Nome, adicional.Nome);
                Assert.Equal(comando.Valor, adicional.Valor);
                Assert.Equal(comando.DataCadastro, adicional.DataCadastro);
                Assert.Equal(comando.DataAlteracao, adicional.DataAlteracao);
                Assert.Equal(comando.Status, adicional.Status);
            }
        }

        public class SetGrupoAdicionalId : AdicionaisTestes
        {
            [Fact]
            public void Quando_GrupoAdicionalIdForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoGrupoAdicionalId = 42;
                // Act
                sut.SetGrupoAdicionalId(novoGrupoAdicionalId);
                // Assert
                Assert.Equal(novoGrupoAdicionalId, sut.GrupoAdicionalId);
            }
        }

        public class SetNome : AdicionaisTestes
        {
            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoNome = "Novo Adicional";
                // Act
                sut.SetNome(novoNome);
                // Assert
                Assert.Equal(novoNome, sut.Nome);
            }

            [Fact]
            public void Quando_NomeForNulo_Espero_ArgumentException()
            {
                // Arrange
                string novoNome = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(novoNome));
                Assert.Equal("Nome do adicional não pode ser vazio.", exception.Message);
            }

            [Fact]
            public void Quando_NomeForVazio_Espero_ArgumentException()
            {
                // Arrange
                string novoNome = "";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(novoNome));
                Assert.Equal("Nome do adicional não pode ser vazio.", exception.Message);
            }
        }

        public class SetValor : AdicionaisTestes
        {
            [Fact]
            public void Quando_ValorForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoValor = 20.0m;
                // Act
                sut.SetValor(novoValor);
                // Assert
                Assert.Equal(novoValor, sut.Valor);
            }

            [Fact]
            public void Quando_ValorForNegativo_Espero_ArgumentException()
            {
                // Arrange
                var novoValor = -5;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetValor(novoValor));
                Assert.Equal("Valor do adicional não pode ser negativo.", exception.Message);
            }
        }

        public class SetStatus : AdicionaisTestes
        {
            [Fact]
            public void Quando_StatusForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoStatus = StatusEnum.Inativo;
                // Act
                sut.SetStatus(novoStatus);
                // Assert
                Assert.Equal(novoStatus, sut.Status);
            }
        }

        public class SetDataAlteracao : AdicionaisTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDataAlteracao = DateTime.UtcNow.AddDays(-1);
                // Act
                sut.SetDataAlteracao(novaDataAlteracao);
                // Assert
                Assert.Equal(novaDataAlteracao, sut.DataAlteracao);
            }
        }

        public class SetDataCadastro : AdicionaisTestes
        {
            [Fact]
            public void Quando_DataCadastroForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDataCadastro = DateTime.UtcNow.AddDays(-1);
                // Act
                sut.SetDataCadastro(novaDataCadastro);
                // Assert
                Assert.Equal(novaDataCadastro, sut.DataCadastro);
            }
        }
    }
}
