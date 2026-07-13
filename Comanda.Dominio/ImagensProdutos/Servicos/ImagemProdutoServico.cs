using Comanda.Dominio.ImagensProdutos.Comandos;
using Comanda.Dominio.ImagensProdutos.Entidades;
using Comanda.Dominio.ImagensProdutos.Repositorios.Interfaces;
using Comanda.Dominio.ImagensProdutos.Servicos.Interfaces;

namespace Comanda.Dominio.ImagensProdutos.Servicos
{
    public class ImagemProdutoServico : IImagemProdutoServico
    {
        private readonly IImagemProdutoRepositorio imagemProdutoRepositorio;
        public ImagemProdutoServico(IImagemProdutoRepositorio imagemProdutoRepositorio)
        {
            this.imagemProdutoRepositorio = imagemProdutoRepositorio;
        }

        public async Task<List<ImagemProduto>> InserirAsync(List<ImagemProdutoComando> comandos, CancellationToken cancellationToken)
        {
            List<ImagemProduto> imagens = comandos.Select(x => new ImagemProduto(x)).ToList();

            await imagemProdutoRepositorio.InserirAsync(imagens, cancellationToken);
            return imagens;
        }
    }
}
