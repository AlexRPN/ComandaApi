using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Repositorios.Filtros;
using Comanda.Dominio.Genericos;

namespace Comanda.Dominio.Categorias.Repositorios.Interfaces
{
    public interface ICategoriaRepositorio : IGenericoRepositorio<Categoria>
    {
        Task<Categoria> InserirAsync(CategoriaComando comando, CancellationToken cancellationToken);
        Task<IQueryable<Categoria>> FiltrarAsync(CategoriaListarFiltro filtro, CancellationToken cancellationToken);
    }
}
