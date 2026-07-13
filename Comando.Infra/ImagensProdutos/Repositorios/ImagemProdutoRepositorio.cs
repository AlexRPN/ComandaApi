using Comanda.Dominio.ImagensProdutos.Entidades;
using Comanda.Dominio.ImagensProdutos.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.ImagensProdutos.Repositorios
{
    public class ImagemProdutoRepositorio : GenericoRepositorio<ImagemProduto>, IImagemProdutoRepositorio
    {
        public ImagemProdutoRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<ImagemProduto>> InserirAsync(List<ImagemProduto> imagens, CancellationToken cancellationToken)
        {
            await appDbContext.ImagensProdutos.AddRangeAsync(imagens, cancellationToken);

            return imagens;
        }
    }
}
