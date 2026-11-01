using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;
using Comanda.Dominio.Produtos.Repositorios.Interfaces;
using Comanda.Dominio.Produtos.Servicos;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.Produtos.Servicos
{
    public class ProdutosServicoTestes
    {
        private readonly ProdutoServico sut;
        private readonly ProdutoComando produtoComando;
        private readonly IProdutoRepositorio produtoRepositorioMock;
        public ProdutosServicoTestes()
        {
            produtoRepositorioMock = Substitute.For<IProdutoRepositorio>();
            produtoComando = Builder<ProdutoComando>
                .CreateNew()
                .With(x => x.EmpresaId = 1)
                .With(x => x.CategoriaId = 1)
                .With(x => x.Nome = "Nome Produto Teste")
                .With(x => x.Descricao = "Descrição produto teste")
                .With(x => x.TempoPreparo = 10)
                .With(x => x.Status = StatusEnum.Ativo)
                .With(x => x.SituacaoProduto = SituacaoProdutoEnum.Disponivel)
                .With(x => x.DataCadastro = DateTime.Now.Date)
                .Build();
            sut = new ProdutoServico(produtoRepositorioMock);
        }

        public class InserirAsyncMetodo : ProdutosServicoTestes
        {
            [Fact]
            public async Task Quando_ProdutoForValido_Espero_ProdutoInseridoAsync()
            {
                var novoProduto = new ProdutoComando
                {
                    EmpresaId = produtoComando.EmpresaId,
                    CategoriaId = produtoComando.CategoriaId,
                    Nome = produtoComando.Nome,
                    Descricao = produtoComando.Descricao,
                    TempoPreparo = produtoComando.TempoPreparo,
                    Status = produtoComando.Status,
                    SituacaoProduto = produtoComando.SituacaoProduto,
                    DataCadastro = produtoComando.DataCadastro
                };

                var produtoInserido = new Produto(novoProduto);

                produtoRepositorioMock.ValidarAsync(Arg.Any<Expression<Func<Produto, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(false);

                produtoRepositorioMock.InserirAsync(Arg.Any<ProdutoComando>(), Arg.Any<CancellationToken>())
                    .Returns(produtoInserido);

                var resultado = await sut.InserirAsync(novoProduto, CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.EmpresaId.Should().Be(produtoComando.EmpresaId);
                resultado.CategoriaId.Should().Be(produtoComando.CategoriaId);
                resultado.Nome.Should().Be(produtoComando.Nome);
                resultado.Descricao.Should().Be(produtoComando.Descricao);
                resultado.TempoPreparo.Should().Be(produtoComando.TempoPreparo);
            }

            [Fact]
            public async Task Quando_ProdutoNaoExistir_Espero_ExceptionAsync()
            {
                produtoRepositorioMock.ValidarAsync(Arg.Any<Expression<Func<Produto, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(true);

                Func<Task> act = async () => await sut.InserirAsync(produtoComando, CancellationToken.None);

                await act.Should().ThrowAsync<Exception>().WithMessage("Produto já cadastrado para esta empresa e categoria.");
            }
        }

        public class FiltrarAsyncMetodo : ProdutosServicoTestes
        {
            [Fact]
            public async Task Quando_ProdutosExistentes_Espero_RetornarProdutosFiltrados()
            {
                var produtos = Builder<Produto>.CreateListOfSize(5).All().Build();
                var queryableProdutos = produtos.AsQueryable();

                produtoRepositorioMock.FiltrarAsync(Arg.Any<ProdutoListarFiltro>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(queryableProdutos));

                var resultado = await sut.FiltrarAsync(new ProdutoListarFiltro(), CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.Count().Should().Be(5);
            }
        }

        public class ListarPaginadoAsyncMetodo : ProdutosServicoTestes
        {
            [Fact]
            public async Task Quando_ProdutosExistentes_Espero_RetornarProdutosPaginados()
            {
                // Arrange
                var produtos = Builder<Produto>.CreateListOfSize(10).All().Build();
                var queryableProdutos = produtos.AsQueryable();

                // Mock do repositório
                produtoRepositorioMock.ListarPaginadoAsync(
                    Arg.Any<IQueryable<Produto>>(), 
                    Arg.Any<int>(), 
                    Arg.Any<int>(), 
                    Arg.Any<string>(), 
                    Arg.Any<TipoOrdenacaoEnum>(), 
                    Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(new PaginacaoConsulta<Produto>
                    {
                        Total = produtos.Count,
                        Registros = produtos
                    }));

                // Act
                var resultado = await sut.ListarPaginadoAsync(queryableProdutos, 10, 1, "Nome", TipoOrdenacaoEnum.Asc, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
            }
        }
    }
}
