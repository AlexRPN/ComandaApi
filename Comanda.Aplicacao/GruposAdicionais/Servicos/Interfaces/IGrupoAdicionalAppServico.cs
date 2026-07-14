using Comanda.DataTransfer.GruposAdicionais.Request;
using Comanda.DataTransfer.GruposAdicionais.Response;

namespace Comanda.Aplicacao.GruposAdicionais.Servicos.Interfaces
{
    public interface IGrupoAdicionalAppServico
    {
        Task<List<GrupoAdicionalResponse>> InserirAsync(List<GrupoAdicionalRequest> requests, CancellationToken cancellationToken);
    }
}
