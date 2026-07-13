using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Repositorios.Filtros;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Categorias.Servicos.Interfaces
{
    public interface ICategoriaServico
    {
        Task<Categoria> InserirAsync(CategoriaComando comando, CancellationToken cancellationToken);
        Task<Categoria> EditarAsync(CategoriaEditarComando comando, CancellationToken cancellationToken);
        Task<Categoria> RecuperarPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IQueryable<Categoria>> FiltrarAsync(CategoriaListarFiltro filtro, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<Categoria>> ListarPaginadoAsync(IQueryable<Categoria> query, int qt, int pg, string cpOrd,
                                                               TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken);
    }
}
