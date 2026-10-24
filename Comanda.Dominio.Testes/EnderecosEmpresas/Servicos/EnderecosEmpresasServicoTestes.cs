
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Repositorios.Interfaces;
using Comanda.Dominio.EnderecosEmpresas.Servicos;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.EnderecosEmpresas.Servicos
{
    public class EnderecosEmpresasServicoTestes
    {
        private readonly EnderecoEmpresaServico sut;
        private readonly EnderecoEmpresaComando enderecoEmpresaComando;
        private readonly IEnderecoEmpresaRepositorio enderecoEmpresaRepositorioMock;
        public EnderecosEmpresasServicoTestes()
        {
            enderecoEmpresaRepositorioMock = Substitute.For<IEnderecoEmpresaRepositorio>();
            enderecoEmpresaComando = Builder<EnderecoEmpresaComando>.CreateNew()
                .With(e => e.Id = 1)
                .With(e => e.EmpresaId = 1)
                .With(e => e.Cep = "74937020")
                .With(e => e.Logradouro = "Logradouro teste")
                .With(e => e.Numero = "2")
                .With(e => e.Complemento = "Complemento teste")
                .With(e => e.Bairro = "Bairro teste")
                .With(e => e.Cidade = "Cidade teste")
                .With(e => e.Estado = "GO")
                .With(e => e.Pais = "Brasil")
                .With(e => e.Latitude = 12.345678m)
                .With(e => e.Longitude = 98.765432m)
                .Build();
            sut = new EnderecoEmpresaServico(enderecoEmpresaRepositorioMock);
        }

        public class InserirAsyncMetodo : EnderecosEmpresasServicoTestes
        {
            [Fact]
            public void Quando_EnderecoEmpresaForInserido_Espero_RetornoEndereco()
            {
                // Arrange
                var comando = new EnderecoEmpresaComando
                {
                    EmpresaId = enderecoEmpresaComando.EmpresaId,
                    Cep = enderecoEmpresaComando.Cep,
                    Logradouro = enderecoEmpresaComando.Logradouro,
                    Numero = enderecoEmpresaComando.Numero,
                    Complemento = enderecoEmpresaComando.Complemento,
                    Bairro = enderecoEmpresaComando.Bairro,
                    Cidade = enderecoEmpresaComando.Cidade,
                    Estado = enderecoEmpresaComando.Estado,
                    Pais = enderecoEmpresaComando.Pais,
                    Latitude = enderecoEmpresaComando.Latitude,
                    Longitude = enderecoEmpresaComando.Longitude
                };

                var novoEnderecoEmpresa = new EnderecoEmpresaInserirComando
                {
                    EmpresaId = comando.EmpresaId,
                    Cep = comando.Cep,
                    Logradouro = comando.Logradouro,
                    Numero = comando.Numero,
                    Complemento = comando.Complemento,
                    Bairro = comando.Bairro,
                    Cidade = comando.Cidade,
                    Estado = comando.Estado,
                    Pais = comando.Pais,
                    Latitude = comando.Latitude,
                    Longitude = comando.Longitude
                };

                // Mock do repositorio para retornar o endereço da empresa esperado ao inserir
                enderecoEmpresaRepositorioMock.InserirAsync(Arg.Any<EnderecoEmpresaComando>(), Arg.Any<CancellationToken>())
                    .Returns(comando);

                // Act
                var resultado = sut.InserirAsync(novoEnderecoEmpresa, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
            }
        }

        public class EditarAsyncMetodo : EnderecosEmpresasServicoTestes
        {
            [Fact]
            public void Quando_EnderecoEmpresaExistir_Espero_EnderecoEditado()
            {
                var enderecoEmpresaExistente = new EnderecoEmpresa(new EnderecoEmpresaComando
                {
                    EmpresaId = enderecoEmpresaComando.EmpresaId,
                    Cep = enderecoEmpresaComando.Cep,
                    Logradouro = enderecoEmpresaComando.Logradouro,
                    Numero = enderecoEmpresaComando.Numero,
                    Complemento = enderecoEmpresaComando.Complemento,
                    Bairro = enderecoEmpresaComando.Bairro,
                    Cidade = enderecoEmpresaComando.Cidade,
                    Estado = enderecoEmpresaComando.Estado,
                    Pais = enderecoEmpresaComando.Pais,
                    Latitude = enderecoEmpresaComando.Latitude,
                    Longitude = enderecoEmpresaComando.Longitude
                });

                var enderecoEditado = new EnderecoEmpresaEditarComando
                {
                    EmpresaId = enderecoEmpresaExistente.EmpresaId,
                    Cep = "74937020",
                    Logradouro = "Novo Logradouro",
                    Numero = "10",
                    Complemento = "Novo Complemento",
                    Bairro = "Bairro teste",
                    Cidade = "Cidade teste",
                    Estado = "Estado teste",
                    Pais = "Pais teste",
                    Latitude = enderecoEmpresaExistente.Latitude,
                    Longitude = enderecoEmpresaExistente.Longitude
                };

                // Mock do repositorio para retornar o endereço da empresa esperado ao editar
                enderecoEmpresaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<EnderecoEmpresa, bool>>>(), Arg.Any<CancellationToken>()).Returns(enderecoEmpresaExistente);

                // Act
                var resultado = sut.EditarAsync(enderecoEditado, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
            }
        }

        public class ValidarAsyncMetodo : EnderecosEmpresasServicoTestes
        {
            [Fact]
            public void Quando_EnderecoEmpresaForValido_Espero_EnderecoEmpresaRetornado()
            {
                // Arrange
                var enderecoEmpresaExistente = new EnderecoEmpresa(new EnderecoEmpresaComando
                {
                    EmpresaId = enderecoEmpresaComando.EmpresaId,
                    Cep = enderecoEmpresaComando.Cep,
                    Logradouro = enderecoEmpresaComando.Logradouro,
                    Numero = enderecoEmpresaComando.Numero,
                    Complemento = enderecoEmpresaComando.Complemento,
                    Bairro = enderecoEmpresaComando.Bairro,
                    Cidade = enderecoEmpresaComando.Cidade,
                    Estado = enderecoEmpresaComando.Estado,
                    Pais = enderecoEmpresaComando.Pais,
                    Latitude = enderecoEmpresaComando.Latitude,
                    Longitude = enderecoEmpresaComando.Longitude
                });

                // Mock do repositorio para retornar o endereço da empresa esperado
                enderecoEmpresaRepositorioMock.RecuperarAsync(enderecoEmpresaComando.Id, CancellationToken.None).Returns(enderecoEmpresaExistente);

                // Act
                var resultado = sut.ValidarAsync(enderecoEmpresaExistente.Id, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
            }
        }
    }
}
