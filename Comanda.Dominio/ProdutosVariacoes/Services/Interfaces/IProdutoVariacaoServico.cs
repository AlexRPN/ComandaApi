using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Entidades;

namespace Comanda.Dominio.ProdutosVariacoes.Services.Interfaces
{
    public interface IProdutoVariacaoServico
    {
        Task<List<ProdutoVariacao>> InserirAsync(List<ProdutoVariacaoComando> comandos, CancellationToken cancellationToken);
    }
}
