using Comanda.Dominio.ImagensProdutos.Comandos;
using Comanda.Dominio.ImagensProdutos.Entidades;

namespace Comanda.Dominio.ImagensProdutos.Servicos.Interfaces
{
    public interface IImagemProdutoServico
    {
        Task<List<ImagemProduto>> InserirAsync(List<ImagemProdutoComando> comandos, CancellationToken cancellationToken);
    }
}
