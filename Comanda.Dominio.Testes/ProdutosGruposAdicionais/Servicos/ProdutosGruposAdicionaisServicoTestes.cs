
using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;
using Comanda.Dominio.ProdutosGruposAdicionais.Repositorios.Interfaces;
using Comanda.Dominio.ProdutosGruposAdicionais.Servicos;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;

namespace Comanda.Dominio.Testes.ProdutosGruposAdicionais.Servicos
{
    public class ProdutosGruposAdicionaisServicoTestes
    {
        private readonly ProdutoGrupoAdicionalServico sut;
        private readonly ProdutoGrupoAdicionalComando comando;
        private readonly IProdutoGrupoAdicionalRepositorio produtoGrupoAdicionalRepositorioMock;
        public ProdutosGruposAdicionaisServicoTestes()
        {
            comando = Builder<ProdutoGrupoAdicionalComando>
                .CreateNew()
                .With(x => x.ProdutoId = 1)
                .With(x => x.GrupoAdicionalId = 1)
                .Build();
            produtoGrupoAdicionalRepositorioMock = Substitute.For<IProdutoGrupoAdicionalRepositorio>();
            sut = new ProdutoGrupoAdicionalServico(produtoGrupoAdicionalRepositorioMock);
        }

        public class InserirAsyncMetodo : ProdutosGruposAdicionaisServicoTestes
        {
            [Fact]
            public async Task Quando_ProdutoGrupoAdicionalValido_Espero_ProdutoGrupoAdicionalInserido()
            {
                var produtoGrupoAdicional = new ProdutoGrupoAdicional(comando);

                produtoGrupoAdicionalRepositorioMock.InserirAsync(Arg.Any<ProdutoGrupoAdicionalComando>(),
                                                                  Arg.Any<CancellationToken>())
                                                    .Returns(produtoGrupoAdicional);

                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.ProdutoId.Should().Be(comando.ProdutoId);
                resultado.GrupoAdicionalId.Should().Be(comando.GrupoAdicionalId);
            }
        }
    }
}
