using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Clientes.Entidades
{
    public class ClientesTestes
    {
        private readonly Cliente sut;
        public ClientesTestes()
        {
            sut = Builder<Cliente>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                // Arrange
                var comando = new ClienteComando
                {
                    EmpresaId = 1,
                    Nome = "Cliente Teste",
                    Telefone = "123456789",
                    PontosFidelidade = 100,
                    DataCadastro = DateTime.UtcNow,
                    DataAlteracao = DateTime.UtcNow,
                    Status = StatusEnum.Ativo
                };
                // Act
                var cliente = new Cliente(comando);
                // Assert
                Assert.Equal(comando.EmpresaId, cliente.EmpresaId);
                Assert.Equal(comando.Nome, cliente.Nome);
                Assert.Equal(comando.Telefone, cliente.Telefone);
                Assert.Equal(comando.PontosFidelidade, cliente.PontosFidelidade);
                Assert.Equal(comando.DataCadastro, cliente.DataCadastro);
                Assert.Equal(comando.DataAlteracao, cliente.DataAlteracao);
                Assert.Equal(comando.Status, cliente.Status);
            }
        }

        public class SetEmpresaId : ClientesTestes
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

        public class SetNome : ClientesTestes
        {
            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoNome = "Cliente Teste";
                // Act
                sut.SetNome(novoNome);
                // Assert
                Assert.Equal(novoNome, sut.Nome);
            }

            [Fact]
            public void Quando_NomeForNuloOuVazio_Espero_Excecao()
            {
                // Arrange
                string nomeInvalido = null;
                // Act & Assert
                var excecao = Assert.Throws<ArgumentException>(() => sut.SetNome(nomeInvalido));
                Assert.Equal("O nome do cliente não pode ser nulo ou vazio.", excecao.Message);
            }
        }

        public class SetTelefone : ClientesTestes
        {
            [Fact]
            public void Quando_TelefoneForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoTelefone = "123456789";
                // Act
                sut.SetTelefone(novoTelefone);
                // Assert
                Assert.Equal(novoTelefone, sut.Telefone);
            }

            [Fact]
            public void Quando_TelefoneForNuloOuVazio_Espero_Excecao()
            {
                // Arrange
                string telefoneInvalido = null;
                // Act & Assert
                var excecao = Assert.Throws<ArgumentException>(() => sut.SetTelefone(telefoneInvalido));
                Assert.Equal("O telefone do cliente não pode ser nulo ou vazio.", excecao.Message);
            }

            [Fact]
            public void Quando_TelefonePossuirMaisDe15Caracteres_Espero_Excecao()
            {
                // Arrange
                var telefoneInvalido = "1234567890123456";

                // Act & Assert
                var excecao = Assert.Throws<ArgumentException>(() => sut.SetTelefone(telefoneInvalido));
                Assert.Equal("O telefone do cliente não pode ter mais de 15 caracteres.", excecao.Message);
            }
        }

        public class SetPontosFidelidade : ClientesTestes
        {
            [Fact]
            public void Quando_PontosFidelidadeForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novosPontosFidelidade = 100;
                // Act
                sut.SetPontosFidelidade(novosPontosFidelidade);
                // Assert
                Assert.Equal(novosPontosFidelidade, sut.PontosFidelidade);
            }
        }

        public class SetDataCadastro : ClientesTestes
        {
            [Fact]
            public void Quando_DataCadastroForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDataCadastro = DateTime.UtcNow;
                // Act
                sut.SetDataCadastro(novaDataCadastro);
                // Assert
                Assert.Equal(novaDataCadastro, sut.DataCadastro);
            }
        }

        public class SetDataAlteracao : ClientesTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDataAlteracao = DateTime.UtcNow;
                // Act
                sut.SetDataAlteracao(novaDataAlteracao);
                // Assert
                Assert.Equal(novaDataAlteracao, sut.DataAlteracao);
            }
        }

        public class SetStatus : ClientesTestes
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
    }
}
