using Comanda.DataTransfer.Produtos.Request;
using Comanda.DataTransfer.Produtos.Response;

namespace Comanda.Aplicacao.Produtos.Servicos.Interfaces
{
    public interface IProdutoAppServico
    {
        Task<IEnumerable<ProdutoListarResponse>> ListarAsync(ProdutoFiltroRequest request, CancellationToken cancellationToken);
        Task<ProdutoResponse> InserirAsync(ProdutoRequest request, CancellationToken cancellationToken);
    }
}
