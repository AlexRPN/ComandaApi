using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Produtos.Servicos.Interfaces
{
    public interface IProdutoServico
    {
        Task<Produto> InserirAsync(ProdutoComando comando, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<Produto>> ListarPaginadoAsync(IQueryable<Produto> query, int qt, int pg, string cpOrd,
                                                                          TipoOrdenacaoEnum tpOrd,
                                                                          CancellationToken cancellationToken);
        Task<IQueryable<Produto>> FiltrarAsync(ProdutoListarFiltro filtro, CancellationToken cancellationToken);
    }
}
