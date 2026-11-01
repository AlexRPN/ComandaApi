
using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Entidades;
using Comanda.Dominio.ProdutosVariacoes.Repositorios.Interfaces;
using Comanda.Dominio.ProdutosVariacoes.Services;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.ProdutosVariacoes.Servicos
{
    public class ProdutosVariacoesServicoTestes
    {
        private readonly ProdutoVariacaoServico sut;
        private readonly ProdutoVariacaoComando produtoVariacaoComando;
        private readonly IProdutoVariacaoRepositorio produtoVariacaoRepositorioMock;
        public ProdutosVariacoesServicoTestes()
        {
            produtoVariacaoComando = Builder<ProdutoVariacaoComando>
                .CreateNew()
                .With(x => x.ProdutoId = 1)
                .With(x => x.Descricao = "Descrição teste")
                .With(x => x.Preco = 12.99m)
                .With(x => x.Ordem = 1)
                .With(x => x.Status = StatusEnum.Ativo)
                .With(x => x.DataCadastro = DateTime.Now.Date)
                .Build();
            produtoVariacaoRepositorioMock = Substitute.For<IProdutoVariacaoRepositorio>();
            sut = new ProdutoVariacaoServico(produtoVariacaoRepositorioMock);
        }

        public class InserirAsyncMetodo : ProdutosVariacoesServicoTestes
        {
            [Fact]
            public async Task Quando_ProdutoVariacaoForValido_Espero_ProdutoInseridoAsync()
            {

                var comando = new List<ProdutoVariacaoComando>
                {
                    new ProdutoVariacaoComando
                    {
                        ProdutoId = produtoVariacaoComando.ProdutoId,
                        Descricao = produtoVariacaoComando.Descricao,
                        Preco = produtoVariacaoComando.Preco,
                        Ordem = produtoVariacaoComando.Ordem,
                        Status = produtoVariacaoComando.Status,
                        DataCadastro = produtoVariacaoComando.DataCadastro
                    }
                };

                var produtoVariacaoInserido = new List<ProdutoVariacao>
                {
                    new ProdutoVariacao(new ProdutoVariacaoComando
                    {
                        ProdutoId = produtoVariacaoComando.ProdutoId,
                        Descricao = produtoVariacaoComando.Descricao,
                        Preco = produtoVariacaoComando.Preco,
                        Ordem = produtoVariacaoComando.Ordem,
                        Status = produtoVariacaoComando.Status,
                        DataCadastro = produtoVariacaoComando.DataCadastro
                    })
                };

                produtoVariacaoRepositorioMock.ValidarAsync(Arg.Any<Expression<Func<ProdutoVariacao, bool>>>(),         
                                                            Arg.Any<CancellationToken>())
                                              .Returns(false);

                produtoVariacaoRepositorioMock.InserirAsync(Arg.Any<List<ProdutoVariacaoComando>>(),
                                                            Arg.Any<CancellationToken>())
                                              .Returns(produtoVariacaoInserido);

                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.ForEach(x => x.ProdutoId.Should().Be(produtoVariacaoComando.ProdutoId));
                resultado.ForEach(x => x.Descricao.Should().Be(produtoVariacaoComando.Descricao));
                resultado.ForEach(x => x.Preco.Should().Be(produtoVariacaoComando.Preco));
                resultado.ForEach(x => x.Ordem.Should().Be(produtoVariacaoComando.Ordem));
                resultado.ForEach(x => x.Status.Should().Be(produtoVariacaoComando.Status));
                resultado.ForEach(x => x.DataCadastro.Should().Be(produtoVariacaoComando.DataCadastro));
            }

            [Fact]
            public async Task Quando_ProdutoVariacaoJaExistir_Espero_Exception()
            {
                var comando = new List<ProdutoVariacaoComando>();

                produtoVariacaoRepositorioMock.ValidarAsync(Arg.Any<Expression<Func<ProdutoVariacao, bool>>>(),
                                                            Arg.Any<CancellationToken>())
                                              .Returns(true);

                Func<Task> act = async () => await sut.InserirAsync(comando, CancellationToken.None);

                await act.Should()
                         .ThrowAsync<Exception>()
                         .WithMessage("Já existe uma variação com a mesma descrição para este produto!");
            }
        }
    }
}
