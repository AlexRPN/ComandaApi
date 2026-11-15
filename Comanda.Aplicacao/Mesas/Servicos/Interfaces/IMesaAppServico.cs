
using Comanda.DataTransfer.Mesas.Request;
using Comanda.DataTransfer.Mesas.Response;

namespace Comanda.Aplicacao.Mesas.Servicos.Interfaces
{
    public interface IMesaAppServico
    {
        Task<string> InserirAsync(MesaRequest request, CancellationToken cancellationToken);
        Task<MesaResponse> EditarAsync(MesaEditarRequest request, CancellationToken cancellationToken);
    }
}
