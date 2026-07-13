using Comanda.Dominio.Genericos;
using Comanda.Dominio.ImagensProdutos.Entidades;

namespace Comanda.Dominio.ImagensProdutos.Repositorios.Interfaces
{
    public interface IImagemProdutoRepositorio : IGenericoRepositorio<ImagemProduto>
    {
        Task<List<ImagemProduto>> InserirAsync(List<ImagemProduto> imagens, CancellationToken cancellationToken);
    }
}
