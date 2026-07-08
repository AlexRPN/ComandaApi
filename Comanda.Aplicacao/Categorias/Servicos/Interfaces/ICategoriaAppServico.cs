using Comanda.DataTransfer.Categorias.Request;
using Comanda.DataTransfer.Categorias.Response;

namespace Comanda.Aplicacao.Categorias.Servicos.Interfaces
{
    public interface ICategoriaAppServico
    {
        Task<CategoriaResponse> InserirAsync(CategoriaRequest request, CancellationToken cancellationToken);
        Task<CategoriaResponse> EditarAsync(CategoriaEditarRequest request, CancellationToken cancellationToken);
    }
}
