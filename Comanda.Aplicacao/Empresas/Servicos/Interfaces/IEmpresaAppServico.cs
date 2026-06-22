using Comanda.DataTransfer.Empresas.Request;
using Comanda.DataTransfer.Empresas.Response;

namespace Comanda.Aplicacao.Empresas.Servicos.Interfaces
{
    public interface IEmpresaAppServico
    {
        Task<EmpresaResponse> InserirAsync(EmpresaRequest request, CancellationToken cancellationToken);
        Task<EmpresaResponse> RecuperarAsync(int id, CancellationToken cancellationToken);
    }
}
