using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;

namespace Comanda.Dominio.Produtos.Repositorios.Interfaces
{
    public interface IProdutoDapperRepositorio
    {
        Task<IEnumerable<Produto>> ListarAsync(ProdutoListarFiltro filtro, CancellationToken cancellationToken);
    }
}
