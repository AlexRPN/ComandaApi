
using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Filtros;
using Comanda.Dominio.Clientes.Repositorios.Interfaces;
using Comanda.Dominio.Clientes.Servicos;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.Clientes.Servicos
{
    public class ClientesServicoTestes
    {
        private readonly ClienteServico sut;
        private readonly ClienteComando clienteComando;
        private readonly IClienteRepositorio clienteRepositorioMock;

        public ClientesServicoTestes()
        {
            clienteRepositorioMock = Substitute.For<IClienteRepositorio>();
            clienteComando = Builder<ClienteComando>.CreateNew()
                .With(c => c.Id = 1)
                .With(c => c.EmpresaId = 1)
                .With(c => c.Nome = "Cliente Teste")
                .With(c => c.Telefone = "123456789")
                .With(c => c.PontosFidelidade = 0)
                .With(c => c.DataCadastro = DateTime.UtcNow)
                .With(c => c.Status = StatusEnum.Ativo)
                .Build();
            sut = new ClienteServico(clienteRepositorioMock);
        }

        public class ValidarTelefoneDuplicadoAsyncMetodo : ClientesServicoTestes
        {
            [Fact]
            public async Task Quando_TelefoneNaoExistir_Espero_NaoLancarExcecao()
            {
                // Arrange
                var empresaId = 1;
                var telefone = "62999999999";

                clienteRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Cliente, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns((Cliente?)null);

                // Act
                Func<Task> act = () => sut.ValidarTelefoneDuplicadoAsync(empresaId, telefone, CancellationToken.None);

                // Assert
                await act.Should().NotThrowAsync();
            }

            [Fact]
            public async Task Quando_TelefoneJaExistir_Espero_Excecao()
            {
                // Arrange
                var empresaId = 1;
                var telefone = "62999999999";

                var clienteExistente = Builder<Cliente>
                    .CreateNew()
                    .Build();

                clienteRepositorioMock
                    .RecuperarAsync(
                        Arg.Any<Expression<Func<Cliente, bool>>>(),
                        Arg.Any<CancellationToken>())
                    .Returns(clienteExistente);

                // Act
                Func<Task> act = () =>
                    sut.ValidarTelefoneDuplicadoAsync(
                        empresaId,
                        telefone,
                        CancellationToken.None);

                // Assert
                await act.Should()
                    .ThrowAsync<Exception>()
                    .WithMessage("Já existe um cliente cadastrado com esse telefone.");
            }
        }

        public class InserirAsyncMetodo : ClientesServicoTestes
        {
            [Fact]
            public async Task Quando_ClienteForInserido_Espero_RetornarCliente()
            {
                // Arrange
                var comando = new ClienteComando
                {
                    EmpresaId = clienteComando.EmpresaId,
                    Nome = clienteComando.Nome,
                    Telefone = clienteComando.Telefone,
                    PontosFidelidade = clienteComando.PontosFidelidade
                };

                var clienteEsperado = new Cliente(new ClienteComando
                {
                    EmpresaId = comando.EmpresaId,
                    Nome = comando.Nome,
                    Telefone = comando.Telefone,
                    PontosFidelidade = comando.PontosFidelidade,
                    DataCadastro = DateTime.UtcNow,
                    Status = StatusEnum.Ativo
                });

                // Mock do repositorio para retornar o cliente esperado ao inserir
                clienteRepositorioMock.InserirAsync(Arg.Any<ClienteComando>(), Arg.Any<CancellationToken>()).Returns(clienteEsperado);

                // Act
                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Nome.Should().Be(clienteEsperado.Nome);
                resultado.Telefone.Should().Be(clienteEsperado.Telefone);
                resultado.PontosFidelidade.Should().Be(clienteEsperado.PontosFidelidade);
                resultado.Status.Should().Be(StatusEnum.Ativo);
                resultado.DataCadastro.Should().BeCloseTo(clienteEsperado.DataCadastro, TimeSpan.FromSeconds(1));
            }
        }

        public class RecuperarAsyncMetodo : ClientesServicoTestes
        {
            [Fact]
            public async Task Quando_ClienteExistir_Espero_RetornarCliente()
            {
                // Arrange
                var clienteExistente = new Cliente(new ClienteComando
                {
                    EmpresaId = clienteComando.EmpresaId,
                    Nome = clienteComando.Nome,
                    Telefone = clienteComando.Telefone,
                    PontosFidelidade = clienteComando.PontosFidelidade,
                    DataCadastro = DateTime.UtcNow,
                    Status = StatusEnum.Ativo
                });
                clienteRepositorioMock.RecuperarPorIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                    .Returns(clienteExistente);
                // Act
                var resultado = await sut.RecuperarAsync(clienteExistente.Id, CancellationToken.None);
                // Assert
                resultado.Should().NotBeNull();
                resultado.Nome.Should().Be(clienteExistente.Nome);
                resultado.Telefone.Should().Be(clienteExistente.Telefone);
            }

            [Fact]
            public async Task Quando_ClienteNaoExistir_Espero_LancarExcecao()
            {
                // Arrange
                clienteRepositorioMock.RecuperarPorIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                    .Returns((Cliente?)null);
                // Act
                Func<Task> act = () => sut.RecuperarAsync(1, CancellationToken.None);
                // Assert
                await act.Should()
                    .ThrowAsync<Exception>()
                    .WithMessage("Cliente não encontrado.");
            }
        }

        public class ListarPaginadoAsyncMetodo : ClientesServicoTestes
        {
            [Fact]
            public async Task Quando_ListarPaginadoAsync_Espero_RetornarPaginacaoConsulta()
            {
                // Arrange
                var query = new List<Cliente>
                {
                    new Cliente(new ClienteComando
                    {
                        EmpresaId = clienteComando.EmpresaId,
                        Nome = clienteComando.Nome,
                        Telefone = clienteComando.Telefone,
                        PontosFidelidade = clienteComando.PontosFidelidade,
                        DataCadastro = DateTime.UtcNow,
                        Status = StatusEnum.Ativo
                    })
                }.AsQueryable();
                var paginacaoEsperada = new PaginacaoConsulta<Cliente>
                {
                    Total = 10,
                    Registros = query.ToList()
                };

                clienteRepositorioMock.ListarPaginadoAsync(Arg.Any<IQueryable<Cliente>>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<TipoOrdenacaoEnum>(), Arg.Any<CancellationToken>())
                    .Returns(paginacaoEsperada);

                // Act
                var resultado = await sut.ListarPaginadoAsync(query, 10, 1, "Nome", TipoOrdenacaoEnum.Asc, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Registros.Should().HaveCount(1);
                resultado.Total.Should().Be(10);
            }
        }

        public class FiltrarAsyncMetodo : ClientesServicoTestes
        {
            [Fact]
            public async Task Quando_FiltrarAsync_Espero_RetornarIQueryable()
            {
                // Arrange
                var filtro = new ClienteListarFiltro
                {
                    Id = clienteComando.Id,
                    EmpresaId = clienteComando.EmpresaId,
                    Nome = clienteComando.Nome,
                    Telefone = clienteComando.Telefone,
                    Status = StatusEnum.Ativo
                };
                var queryEsperada = new List<Cliente>
                {
                    new Cliente(new ClienteComando
                    {
                        EmpresaId = (int)filtro.EmpresaId,
                        Nome = filtro.Nome,
                        Telefone = filtro.Telefone,
                        PontosFidelidade = clienteComando.PontosFidelidade,
                        DataCadastro = DateTime.UtcNow,
                        Status = (StatusEnum)filtro.Status
                    })
                }.AsQueryable();
                clienteRepositorioMock.FiltrarAsync(Arg.Any<ClienteListarFiltro>(), Arg.Any<CancellationToken>())
                    .Returns(queryEsperada);
                // Act
                var resultado = await sut.FiltrarAsync(filtro, CancellationToken.None);
                // Assert
                resultado.Should().NotBeNull();
                resultado.Should().HaveCount(1);
            }
        }

        public class EditarAsyncMetodo : ClientesServicoTestes
        {
            [Fact]
            public async Task Quando_ClienteExistir_Espero_RetornarClienteEditado()
            {
                // Arrange
                var clienteExistente = new Cliente(new ClienteComando
                {
                    EmpresaId = clienteComando.EmpresaId,
                    Nome = clienteComando.Nome,
                    Telefone = clienteComando.Telefone,
                    PontosFidelidade = clienteComando.PontosFidelidade,
                    DataCadastro = DateTime.UtcNow,
                    Status = StatusEnum.Ativo
                });
                var comandoEdicao = new ClienteEditarComando
                {
                    Id = clienteExistente.Id,
                    EmpresaId = clienteExistente.EmpresaId,
                    Nome = "Nome Editado",
                    Telefone = "987654321",
                };
                clienteRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Cliente, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(clienteExistente);
                // Act
                var resultado = await sut.EditarAsync(comandoEdicao, CancellationToken.None);
                // Assert
                resultado.Should().NotBeNull();
                resultado.Nome.Should().Be(comandoEdicao.Nome);
                resultado.Telefone.Should().Be(comandoEdicao.Telefone);
            }

            [Fact]
            public async Task Quando_ClienteNaoExistir_Espero_LancarExcecao()
            {
                // Arrange
                var comandoEdicao = new ClienteEditarComando
                {
                    Id = 1,
                    EmpresaId = 1,
                    Nome = "Nome Editado",
                    Telefone = "987654321",
                };
                clienteRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Cliente, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns((Cliente?)null);
                // Act
                Func<Task> act = () => sut.EditarAsync(comandoEdicao, CancellationToken.None);
                // Assert
                await act.Should()
                    .ThrowAsync<Exception>()
                    .WithMessage("Cliente não encontrado!");
            }
        }

        public class AlterarStatusAsyncMetodo : ClientesServicoTestes
        {
            [Fact]
            public async Task Quando_ClienteExistir_Espero_RetornarClienteComStatusAlterado()
            {
                // Arrange
                var novoStatus = StatusEnum.Inativo;
                var idCliente = 1;

                var comandoEdicao = new ClienteEditarComando
                {
                    Id = clienteComando.Id,
                    EmpresaId = clienteComando.EmpresaId,
                    Nome = clienteComando.Nome,
                    Telefone = clienteComando.Telefone
                };

                var clienteEsperado = new Cliente(new ClienteComando
                {
                    EmpresaId = comandoEdicao.EmpresaId,
                    Nome = comandoEdicao.Nome,
                    Telefone = comandoEdicao.Telefone,
                    PontosFidelidade = clienteComando.PontosFidelidade,
                    DataCadastro = DateTime.UtcNow,
                    Status = StatusEnum.Ativo
                });

                clienteRepositorioMock.RecuperarPorIdAsync(idCliente, Arg.Any<CancellationToken>())
                    .Returns(clienteEsperado);

                // Act
                var resultado = await sut.AlterarStatusAsync(idCliente, novoStatus, CancellationToken.None);

                // Assert
                clienteEsperado.Status.Should().Be(novoStatus);
                await clienteRepositorioMock.Received(1).EditarAsync(clienteEsperado, Arg.Any<CancellationToken>());

                resultado.Should().NotBeNull();
            }

            [Fact]
            public async Task Quando_ClienteNaoExistir_Espero_LancarExcecao()
            {
                // Arrange
                var novoStatus = StatusEnum.Inativo;
                clienteRepositorioMock.RecuperarPorIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                    .Returns((Cliente?)null);
                // Act
                Func<Task> act = () => sut.AlterarStatusAsync(1, novoStatus, CancellationToken.None);
                // Assert
                await act.Should()
                    .ThrowAsync<Exception>()
                    .WithMessage("Cliente não encontrado!");
            }
        }
    }
}
