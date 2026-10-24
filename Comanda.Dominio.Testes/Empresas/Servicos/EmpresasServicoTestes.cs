
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Empresas.Servicos;
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.Empresas.Servicos
{
    public class EmpresasServicoTestes
    {
        private readonly EmpresaServico sut;
        private readonly EmpresaComando empresaComando;
        private readonly IEmpresaRepositorio empresaRepositorioMock;
        public EmpresasServicoTestes()
        {
            empresaComando = Builder<EmpresaComando>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.NomeFantasia = "Empresa Teste")
                .With(x => x.RazaoSocial = "Empresa Teste LTDA")
                .With(x => x.Cnpj = "12345678000195")
                .With(x => x.InscricaoEstadual = "1234567890")
                .With(x => x.Telefone = "6212345678")
                .With(x => x.Email = "teste@empresa.com")
                .With(x => x.Status = StatusEnum.Ativo)
                .With(x => x.Logo = "logo.png")
                .With(x => x.BannerPrincipal = "banner.png")
                .Build();
            empresaRepositorioMock = Substitute.For<IEmpresaRepositorio>();
            sut = new EmpresaServico(empresaRepositorioMock);
        }

        public class InserirAsyncMetodo : EmpresasServicoTestes
        {
            [Fact]
            public async Task Quando_EmpresaValida_Espero_EmpresaInserida()
            {
                // Arrange
                var empresaInserirComando = new EmpresaInserirComando
                {
                    NomeFantasia = empresaComando.NomeFantasia,
                    RazaoSocial = empresaComando.RazaoSocial,
                    Cnpj = empresaComando.Cnpj,
                    InscricaoEstadual = empresaComando.InscricaoEstadual,
                    Telefone = empresaComando.Telefone,
                    Email = empresaComando.Email
                };

                // Mock do repositório para retornar a empresa inserida
                empresaRepositorioMock.InserirAsync(Arg.Any<EmpresaComando>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(empresaComando));

                // Act
                var resultado = await sut.InserirAsync(empresaInserirComando, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.NomeFantasia.Should().Be(empresaComando.NomeFantasia);
                resultado.RazaoSocial.Should().Be(empresaComando.RazaoSocial);
                resultado.Cnpj.Should().Be(empresaComando.Cnpj);
                resultado.InscricaoEstadual.Should().Be(empresaComando.InscricaoEstadual);
                resultado.Telefone.Should().Be(empresaComando.Telefone);
                resultado.Email.Should().Be(empresaComando.Email);
                resultado.Logo.Should().Be(empresaComando.Logo);
                resultado.BannerPrincipal.Should().Be(empresaComando.BannerPrincipal);
            }
        }

        public class EditarAsyncMetodo : EmpresasServicoTestes
        {
            [Fact]
            public async Task Quando_EmpresaValida_Espero_EmpresaEditada()
            {
                // Arrange

                var empresaEditarComando = new EmpresaEditarComando
                {
                    Id = 1,
                    NomeFantasia = empresaComando.NomeFantasia,
                    RazaoSocial = empresaComando.RazaoSocial,
                    Cnpj = empresaComando.Cnpj,
                    InscricaoEstadual = empresaComando.InscricaoEstadual,
                    Telefone = empresaComando.Telefone,
                    Email = empresaComando.Email,
                };

                // Mock do repositório para retornar a empresa existente
                var empresaExistente = new Empresa(empresaComando);
                empresaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Empresa, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(empresaExistente);

                // Act
                var resultado = await sut.EditarAsync(empresaEditarComando, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.NomeFantasia.Should().Be(empresaEditarComando.NomeFantasia);
                resultado.RazaoSocial.Should().Be(empresaEditarComando.RazaoSocial);
                resultado.Cnpj.Should().Be(empresaEditarComando.Cnpj);
                resultado.InscricaoEstadual.Should().Be(empresaEditarComando.InscricaoEstadual);
                resultado.Telefone.Should().Be(empresaEditarComando.Telefone);
                resultado.Email.Should().Be(empresaEditarComando.Email);
            }
        }

        public class ValidarAsyncMetodo : EmpresasServicoTestes
        {
            [Fact]
            public async Task Quando_EmpresaInativa_Espero_ArgumentException()
            {
                // Arrange
                empresaComando.Status = StatusEnum.Inativo;
                var empresaInativa = new Empresa(empresaComando);

                // Mock do repositório para retornar a empresa inativa
                empresaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Empresa, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(empresaInativa);

                // Act
                Func<Task> act = async () => await sut.ValidarAsync(empresaInativa.Id, CancellationToken.None);

                // Assert
                await act.Should().ThrowAsync<ArgumentException>()
                    .WithMessage("Empresa não está ativa.");
            }

            [Fact]
            public async Task Quando_EmpresaAtiva_Espero_EmpresaRetornada()
            {
                // Arrange
                empresaComando.Status = StatusEnum.Ativo;
                var empresaAtiva = new Empresa(empresaComando);

                // Mock do repositório para retornar a empresa ativa
                empresaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Empresa, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(empresaAtiva);

                // Act
                var resultado = await sut.ValidarAsync(empresaAtiva.Id, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Status.Should().Be(StatusEnum.Ativo);
            }

            [Fact]
            public async Task Quando_EmpresaBloqueada_Espero_ArgumentException()
            {
                // Arrange
                empresaComando.Status = StatusEnum.Bloqueado;
                var empresaBloqueada = new Empresa(empresaComando);

                // Mock do repositório para retornar a empresa bloqueada
                empresaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Empresa, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(empresaBloqueada);

                // Act
                Func<Task> act = async () => await sut.ValidarAsync(empresaBloqueada.Id, CancellationToken.None);

                // Assert
                await act.Should().ThrowAsync<ArgumentException>()
                    .WithMessage("Empresa não está ativa.");
            }
        }

        public class ListarAsyncMetodo : EmpresasServicoTestes
        {
            [Fact]
            public async Task Quando_EmpresasExistentes_Espero_RetornarPaginacaoConsulta()
            {
                // Arrange
                var empresas = Builder<Empresa>.CreateListOfSize(10).All().Build();
                var queryableEmpresas = empresas.AsQueryable();

                // Mock do repositório para retornar a lista de empresas
                empresaRepositorioMock.ListarAsync(Arg.Any<IQueryable<Empresa>>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<TipoOrdenacaoEnum>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(new PaginacaoConsulta<Empresa>
                    {
                        Total = empresas.Count,
                        Registros = empresas
                    }));

                // Act
                var resultado = await sut.ListarAsync(queryableEmpresas, 10, 1, "NomeFantasia", TipoOrdenacaoEnum.Asc, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
            }
        }

        public class FiltrarAsyncMetodo : EmpresasServicoTestes
        {
            [Fact]
            public async Task Quando_EmpresasExistentes_Espero_RetornarIQueryable()
            {
                // Arrange
                var empresas = Builder<Empresa>.CreateListOfSize(5).All().Build();
                var queryableEmpresas = empresas.AsQueryable();

                // Mock do repositório para retornar a lista de empresas
                empresaRepositorioMock.FiltrarAsync(Arg.Any<EmpresaListarFiltro>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(queryableEmpresas));

                // Act
                var resultado = await sut.FiltrarAsync(new EmpresaListarFiltro(), CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Count().Should().Be(5);
            }
        }
    }
}
