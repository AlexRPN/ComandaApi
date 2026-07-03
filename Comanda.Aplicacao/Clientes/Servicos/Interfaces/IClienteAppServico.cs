using Comanda.DataTransfer.Clientes.Request;
using Comanda.DataTransfer.Clientes.Response;
using Comanda.Dominio.Utils.Consultas;

namespace Comanda.Aplicacao.Clientes.Servicos.Interfaces
{
    public interface IClienteAppServico
    {
        Task<string> InserirAsync(ClienteRequest request, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<ClienteResponse>> ListarAsync(ClienteListarRequest request, CancellationToken cancellationToken);
    }
}
