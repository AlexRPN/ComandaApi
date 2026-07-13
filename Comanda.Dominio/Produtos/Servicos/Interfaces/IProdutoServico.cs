using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;

namespace Comanda.Dominio.Produtos.Servicos.Interfaces
{
    public interface IProdutoServico
    {
        Task<Produto> InserirAsync(ProdutoComando comando, CancellationToken cancellationToken);
        Task<IEnumerable<Produto>> ListarAsync(ProdutoListarFiltro filtro, CancellationToken cancellationToken);
    }
}
