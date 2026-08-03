using Comanda.DataTransfer.Categorias.Request;
using Comanda.DataTransfer.Categorias.Response;
using Comanda.DataTransfer.Utils.Status.Request;
using Comanda.Dominio.Utils.Consultas;

namespace Comanda.Aplicacao.Categorias.Servicos.Interfaces
{
    public interface ICategoriaAppServico
    {
        Task<CategoriaResponse> InserirAsync(CategoriaRequest request, CancellationToken cancellationToken);
        Task<CategoriaResponse> EditarAsync(CategoriaEditarRequest request, CancellationToken cancellationToken);
        Task<CategoriaResponse> RecuperarPorIdAsync(int id, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<CategoriaListarResponse>> ListarPaginadoAsync(CategoriaListarRequest request,
                                                                             CancellationToken cancellationToken);
        Task<string> AlterarStatusAsync(AlterarStatusRequest request, CancellationToken cancellationToken);
    }
}
