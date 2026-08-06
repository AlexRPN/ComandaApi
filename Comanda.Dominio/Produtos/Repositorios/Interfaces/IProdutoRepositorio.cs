using Comanda.Dominio.Genericos;
using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;

namespace Comanda.Dominio.Produtos.Repositorios.Interfaces
{
    public interface IProdutoRepositorio : IGenericoRepositorio<Produto>
    {
        Task<Produto> InserirAsync(ProdutoComando comando, CancellationToken cancellationToken);
        Task<IQueryable<Produto>> FiltrarAsync(ProdutoListarFiltro filtro, CancellationToken cancellationToken);
    }
}
