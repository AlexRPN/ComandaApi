
using Comanda.Dominio.ImagensProdutos.Comandos;
using Comanda.Dominio.ImagensProdutos.Entidades;
using Comanda.Dominio.ImagensProdutos.Repositorios.Interfaces;
using Comanda.Dominio.ImagensProdutos.Servicos;
using FizzWare.NBuilder;
using NSubstitute;

namespace Comanda.Dominio.Testes.ImagensProdutos.Servicos
{
    public class ImagensProdutosServicoTestes
    {
        private readonly ImagemProdutoServico sut;
        private readonly ImagemProdutoComando comando;
        private readonly IImagemProdutoRepositorio imagemProdutoRepositorioMock;
        public ImagensProdutosServicoTestes()
        {
            comando = Builder<ImagemProdutoComando>
                .CreateNew()
                .With(c => c.Id = 1)
                .With(c => c.ProdutoId = 1)
                .With(c => c.CaminhoArquivo = "/imagens/imagem_teste.jpg")
                .With(c => c.UrlImagem = "http://localhost:5000/imagens/imagem_teste.jpg")
                .With(c => c.DataCadastro = DateTime.Now.Date)
                .Build();
            imagemProdutoRepositorioMock = Substitute.For<IImagemProdutoRepositorio>();
            sut = new ImagemProdutoServico(imagemProdutoRepositorioMock);
        }

        public class InserirAsyncMetodo : ImagensProdutosServicoTestes
        {
            [Fact]
            public async Task Quando_ImagemProdutoForValida_Espero_ImagemProdutoInseridaAsync()
            {
                var imagens = new List<ImagemProdutoComando> { comando };
                var imagensProdutos = imagens.Select(i => new ImagemProduto(i)).ToList();

                imagemProdutoRepositorioMock.InserirAsync(Arg.Any<List<ImagemProduto>>(), Arg.Any<CancellationToken>())
                    .Returns(imagensProdutos);

                var resultado = await sut.InserirAsync(imagens, CancellationToken.None);

                Assert.NotNull(resultado);
                await imagemProdutoRepositorioMock.Received(1)
                    .InserirAsync(Arg.Any<List<ImagemProduto>>(), Arg.Any<CancellationToken>());
            }
        }
    }
}
