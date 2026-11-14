
using Comanda.DataTransfer.Mesas.Request;

namespace Comanda.Aplicacao.Mesas.Servicos.Interfaces
{
    public interface IMesaAppServico
    {
        Task<string> InserirAsync(MesaRequest request, CancellationToken cancellationToken);
    }
}
