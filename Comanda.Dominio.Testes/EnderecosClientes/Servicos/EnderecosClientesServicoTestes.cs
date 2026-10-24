
using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Entidades;
using Comanda.Dominio.EnderecoClientes.Repositorios.Interfaces;
using Comanda.Dominio.EnderecoClientes.Servicos;
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.EnderecosClientes.Servicos
{
    public class EnderecosClientesServicoTestes
    {
        private readonly EnderecoClienteServico sut;
        private readonly EnderecoClienteComando enderecoClienteComando;
        private readonly IEnderecoClienteRepositorio enderecoClienteRepositorioMock;
        public EnderecosClientesServicoTestes()
        {
            enderecoClienteComando = Builder<EnderecoClienteComando>
                .CreateNew()
                .With(e => e.Id = 1)
                .With(e => e.ClienteId = 1)
                .With(e => e.Cep = "74937020")
                .With(e => e.Logradouro = "Logradouro teste")
                .With(e => e.Numero = "2")
                .With(e => e.Complemento = "Complemento teste")
                .With(e => e.Bairro = "Bairro teste")
                .With(e => e.Cidade = "Cidade teste")
                .With(e => e.Estado = "GO")
                .With(e => e.PontoReferencia = "Ponto referência teste")
                .Build();
            enderecoClienteRepositorioMock = Substitute.For<IEnderecoClienteRepositorio>();
            sut = new EnderecoClienteServico(enderecoClienteRepositorioMock);
        }

        public class InserirAsyncMetodo : EnderecosClientesServicoTestes
        {
            [Fact]
            public async Task Quando_EnderecoClienteForInserido_Espero_RetornoEndereco()
            {
                var comando = new EnderecoClienteComando
                {
                    ClienteId = enderecoClienteComando.ClienteId,
                    Cep = enderecoClienteComando.Cep,
                    Logradouro = enderecoClienteComando.Logradouro,
                    Numero = enderecoClienteComando.Numero,
                    Complemento = enderecoClienteComando.Complemento,
                    Bairro = enderecoClienteComando.Bairro,
                    Cidade = enderecoClienteComando.Cidade,
                    Estado = enderecoClienteComando.Estado,
                    PontoReferencia = enderecoClienteComando.PontoReferencia,
                };

                // Mock do repositorio para retornar o endereço do cliente esperado ao inserir
                enderecoClienteRepositorioMock.InserirAsync(Arg.Any<EnderecoClienteComando>(), Arg.Any<CancellationToken>())
                    .Returns(comando);

                // Act
                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
            }

            [Fact]
            public async Task Quando_EnderecoClienteForNulo_Espero_Exception()
            {
                // Arrange
                EnderecoClienteComando? comando = null;

                // Act
                Func<Task> act = () => sut.InserirAsync(comando!, CancellationToken.None);

                // Assert
                await act.Should().ThrowAsync<Exception>().WithMessage("O endereço do cliente precisa estar preenchido!");

                await enderecoClienteRepositorioMock
                    .DidNotReceive()
                    .InserirAsync(
                        Arg.Any<EnderecoClienteComando>(),
                        Arg.Any<CancellationToken>());
            }
        }

        public class EditarAsyncMetodo : EnderecosClientesServicoTestes
        {
            [Fact]
            public async Task Quando_EnderecoClienteExistir_Espero_EnderecoClienteEditado()
            {
                // Arrange
                var enderecoEditado = new EnderecoClienteEditarComando
                {
                    ClienteId = enderecoClienteComando.Id,
                    Cep = "74900000",
                    Logradouro = "Rua Alterada",
                    Numero = "123",
                    Complemento = "Casa 1",
                    Bairro = "Centro",
                    Cidade = "Goiânia",
                    Estado = "GO",
                    PontoReferencia = "Próximo à praça"
                };

                var enderecoExistente = new EnderecoCliente(enderecoClienteComando);

                enderecoClienteRepositorioMock
                    .RecuperarAsync(
                        Arg.Any<Expression<Func<EnderecoCliente, bool>>>(),
                        Arg.Any<CancellationToken>())
                    .Returns(enderecoExistente);

                enderecoClienteRepositorioMock
                    .EditarAsync(
                        Arg.Any<EnderecoCliente>(),
                        Arg.Any<CancellationToken>())
                    .Returns(callInfo => callInfo.Arg<EnderecoCliente>());

                // Act
                var resultado = await sut.EditarAsync(enderecoEditado, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Cep.Should().Be(enderecoEditado.Cep);
                resultado.Logradouro.Should().Be(enderecoEditado.Logradouro);
                resultado.Numero.Should().Be(enderecoEditado.Numero);
                resultado.Complemento.Should().Be(enderecoEditado.Complemento);
                resultado.Bairro.Should().Be(enderecoEditado.Bairro);
                resultado.Cidade.Should().Be(enderecoEditado.Cidade);
                resultado.Estado.Should().Be(enderecoEditado.Estado);
                resultado.PontoReferencia.Should().Be(enderecoEditado.PontoReferencia);
            }

            [Fact]
            public async Task Quando_EnderecoClienteNaoExistir_Espero_NullReferenceException()
            {
                // Arrange
                EnderecoClienteEditarComando? comando = null;

                enderecoClienteRepositorioMock
                    .RecuperarAsync(
                        Arg.Any<Expression<Func<EnderecoCliente, bool>>>(),
                        Arg.Any<CancellationToken>())
                    .Returns((EnderecoCliente?)null);

                // Act
                Func<Task> act = () => sut.EditarAsync(comando, CancellationToken.None);

                // Assert
                await act.Should().ThrowAsync<Exception>().WithMessage("Endereço do cliente não encontrado!");
            }
        }
    }
}
