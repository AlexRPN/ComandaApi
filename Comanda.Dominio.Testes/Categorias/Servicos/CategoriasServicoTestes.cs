using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Repositorios.Filtros;
using Comanda.Dominio.Categorias.Repositorios.Interfaces;
using Comanda.Dominio.Categorias.Servicos;
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.Categorias.Servicos
{
    public class CategoriasServicoTestes
    {
        private readonly CategoriaServico sut;
        private readonly CategoriaComando categoriaComando;
        private readonly EmpresaComando empresaComando;
        private readonly ICategoriaRepositorio categoriaRepositorioMock;
        private readonly IEmpresaRepositorio empresaRepositorioMock;
        public CategoriasServicoTestes()
        {
            categoriaComando = Builder<CategoriaComando>.CreateNew()
                .With(x => x.EmpresaId = 1)
                .With(x => x.Nome = "Categoria Teste")
                .With(x => x.Descricao = "Descrição da Categoria Teste")
                .With(x => x.Status = StatusEnum.Ativo)
                .With(x => x.DataCadastro = DateTime.UtcNow)
                .Build();
            empresaComando = Builder<EmpresaComando>.CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.NomeFantasia = "Empresa Teste")
                .With(x => x.RazaoSocial = "Empresa Teste LTDA")
                .With(x => x.Cnpj = "12345678000199")
                .With(x => x.InscricaoEstadual = "123456789")
                .With(x => x.Email = "empresa@teste.com")
                .With(x => x.Telefone = "62999887766")
                .With(x => x.Status = StatusEnum.Ativo)
                .With(x => x.DataCadastro = DateTime.UtcNow)
                .Build();
            categoriaRepositorioMock = Substitute.For<ICategoriaRepositorio>();
            empresaRepositorioMock = Substitute.For<IEmpresaRepositorio>();
            sut = new CategoriaServico(categoriaRepositorioMock, empresaRepositorioMock);
        }

        public class InserirAsyncMetodo : CategoriasServicoTestes
        {
            [Fact]
            public async Task Quando_CategoriaValido_Espero_CategoriaInserido()
            {
                // Arrange
                var empresaValida = new Empresa(empresaComando);

                // Mock para simular a recuperação da empresa válida do repositório
                empresaRepositorioMock.RecuperarAsync(categoriaComando.EmpresaId, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(empresaValida));
                categoriaRepositorioMock.InserirAsync(Arg.Any<CategoriaComando>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(new Categoria(categoriaComando)));

                // Act
                var resultado = await sut.InserirAsync(categoriaComando, CancellationToken.None);
                // Assert
                Assert.NotNull(resultado);
                Assert.Equal(categoriaComando.Nome, resultado.Nome);
                Assert.Equal(categoriaComando.Descricao, resultado.Descricao);
                Assert.Equal(categoriaComando.Status, resultado.Status);
                Assert.Equal(categoriaComando.DataCadastro, resultado.DataCadastro);
            }
        }

        public class EditarAsyncMetodo : CategoriasServicoTestes
        {
            [Fact]
            public async Task Quando_CategoriaValido_Espero_CategoriaEditado()
            {
                // Arrange
                var categoriaExistente = new Categoria(categoriaComando);
                var categoriaEditada = new CategoriaComando
                {
                    EmpresaId = categoriaExistente.EmpresaId,
                    Nome = "Categoria Editada",
                    Descricao = "Descrição da Categoria Editada",
                    Status = StatusEnum.Ativo,
                    DataCadastro = categoriaExistente.DataCadastro
                };

                var categoriaEditarComando = new CategoriaEditarComando
                {
                    Id = categoriaExistente.Id,
                    EmpresaId = categoriaEditada.EmpresaId,
                    Nome = categoriaEditada.Nome,
                    Descricao = categoriaEditada.Descricao,
                    DataAlteracao = DateTime.Now
                };

                // Mock para simular a recuperação da categoria existente do repositório
                categoriaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Categoria, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(categoriaExistente));

                categoriaRepositorioMock.EditarAsync(Arg.Any<Categoria>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(new Categoria(categoriaEditada)));

                // Act
                var resultado = await sut.EditarAsync(categoriaEditarComando, CancellationToken.None);
                // Assert
                Assert.NotNull(resultado);
                Assert.Equal(categoriaEditada.Nome, resultado.Nome);
                Assert.Equal(categoriaEditada.Descricao, resultado.Descricao);
            }

            [Fact]
            public async Task Quando_CategoriaInvalido_Espero_ArgumentNullException()
            {
                // Arrange
                var categoriaInexistenteId = 999;
                var categoriaEditarComando = new CategoriaEditarComando
                {
                    EmpresaId = 1,
                    Nome = "Categoria Editada",
                    Descricao = "Descrição da Categoria Editada",
                    DataAlteracao = DateTime.Now
                };
                // Mock para simular a recuperação de uma categoria inexistente do repositório
                categoriaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Categoria, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Categoria>(null));
                // Act & Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.EditarAsync(categoriaEditarComando, CancellationToken.None));
            }
        }

        public class RecuperarPorIdAsyncMetodo : CategoriasServicoTestes
        {
            [Fact]
            public async Task Quando_CategoriaValido_Espero_CategoriaRecuperado()
            {
                // Arrange
                var categoriaExistente = new Categoria(categoriaComando);
                // Mock para simular a recuperação da categoria existente do repositório
                categoriaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Categoria, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(categoriaExistente));
                // Act
                var resultado = await sut.RecuperarPorIdAsync(categoriaExistente.Id, CancellationToken.None);
                // Assert
                Assert.NotNull(resultado);
                Assert.Equal(categoriaExistente.Nome, resultado.Nome);
                Assert.Equal(categoriaExistente.Descricao, resultado.Descricao);
            }

            [Fact]
            public async Task Quando_CategoriaInvalido_Espero_ArgumentNullException()
            {
                // Arrange
                var categoriaInexistenteId = 999;
                // Mock para simular a recuperação de uma categoria inexistente do repositório
                categoriaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Categoria, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Categoria>(null));
                // Act & Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.RecuperarPorIdAsync(categoriaInexistenteId, CancellationToken.None));
            }
        }

        public class FiltrarAsyncMetodo : CategoriasServicoTestes
        {
            [Fact]
            public async Task Quando_CategoriaValido_Espero_CategoriaFiltrado()
            {
                // Arrange
                var categoriaExistente = new Categoria(categoriaComando);
                var filtro = new CategoriaListarFiltro
                {
                    EmpresaId = categoriaExistente.EmpresaId,
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao,
                    DataCadastro = categoriaExistente.DataCadastro
                };

                var paginacaoEsperada = new PaginacaoConsulta<Categoria>
                {
                    Total = 10,
                    Registros = new List<Categoria> { categoriaExistente }
                };

                // Criando uma lista de categorias filtradas com a categoria existente
                var categoriasFiltradas = new List<Categoria> { categoriaExistente }.AsQueryable();

                // Mock para simular a filtragem de categorias do repositório
                categoriaRepositorioMock.ListarPaginadoAsync(Arg.Any<IQueryable<Categoria>>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<TipoOrdenacaoEnum>(), Arg.Any<CancellationToken>())
                    .Returns(paginacaoEsperada);

                // Act
                var resultado = await sut.ListarPaginadoAsync(categoriasFiltradas, 10, 1, "Nome", TipoOrdenacaoEnum.Asc, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Registros.Should().HaveCount(1);
                resultado.Total.Should().Be(10);
            }
        }

        public class ListarPaginadoAsyncMetodo : CategoriasServicoTestes
        {
            [Fact]
            public async Task Quando_CategoriaValido_Espero_CategoriaListarPaginado()
            {
                // Arrange
                var categoriaExistente = new Categoria(categoriaComando);
                var filtro = new CategoriaListarFiltro
                {
                    Id = categoriaExistente.Id,
                    EmpresaId = categoriaExistente.EmpresaId,
                    Nome = categoriaExistente.Nome,
                    Descricao = categoriaExistente.Descricao,
                    DataCadastro = categoriaExistente.DataCadastro
                };

                // Criando uma lista de categorias filtradas com a categoria existente
                var categoriasFiltradas = new List<Categoria> { categoriaExistente }.AsQueryable();

                // Mock para simular a filtragem de categorias do repositório
                categoriaRepositorioMock.FiltrarAsync(filtro, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(categoriasFiltradas));

                // Act
                var resultado = await sut.FiltrarAsync(filtro, CancellationToken.None);

                // Assert
                Assert.NotNull(resultado);
                Assert.Single(resultado);
                Assert.Equal(categoriaExistente.Nome, resultado.First().Nome);
                Assert.Equal(categoriaExistente.Descricao, resultado.First().Descricao);
            }
        }

        public class AlterarStatusAsyncMetodo : CategoriasServicoTestes
        {
            [Fact]
            public async Task Quando_CategoriaValido_Espero_StatusAlterado()
            {
                // Arrange
                var categoriaExistente = new Categoria(categoriaComando);
                var novoStatus = StatusEnum.Inativo;

                // Mock para simular a recuperação da categoria existente do repositório
                categoriaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Categoria, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(categoriaExistente));

                categoriaRepositorioMock.EditarAsync(Arg.Any<Categoria>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(new Categoria(categoriaComando)));

                // Act
                var resultado = await sut.AlterarStatusAsync(categoriaExistente.Id, novoStatus, CancellationToken.None);

                // Assert
                Assert.NotNull(resultado);
            }

            [Fact]
            public async Task Quando_CategoriaForNulo_Espero_ArgumentNullException()
            {
                // Arrange
                var categoriaInexistenteId = 999;
                var novoStatus = StatusEnum.Inativo;

                // Mock para simular a recuperação de uma categoria inexistente do repositório
                categoriaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Categoria, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Categoria>(null));

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => sut.AlterarStatusAsync(categoriaInexistenteId, novoStatus, CancellationToken.None));
            }
        }
    }
}
