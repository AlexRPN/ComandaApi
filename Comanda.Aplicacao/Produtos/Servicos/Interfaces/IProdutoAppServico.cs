using Comanda.DataTransfer.Produtos.Request;
using Comanda.DataTransfer.Produtos.Response;
using Comanda.Dominio.Utils.Consultas;

namespace Comanda.Aplicacao.Produtos.Servicos.Interfaces
{
    public interface IProdutoAppServico
    {
        Task<ProdutoResponse> InserirAsync(ProdutoRequest request, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<ProdutoListarResponse>> ListarPaginadoAsync(ProdutoListarRequest request, 
                                                                           CancellationToken cancellationToken);
    }
}
