using Comanda.Dominio.Genericos;
using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Entidades;

namespace Comanda.Dominio.ProdutosVariacoes.Repositorios.Interfaces
{
    public interface IProdutoVariacaoRepositorio : IGenericoRepositorio<ProdutoVariacao>
    {
        Task<List<ProdutoVariacao>> InserirAsync(List<ProdutoVariacaoComando> comandos, CancellationToken cancellationToken);
    }
}
