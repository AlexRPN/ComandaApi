using Comanda.DataTransfer.Clientes.Request;
using Comanda.DataTransfer.Clientes.Response;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Aplicacao.Clientes.Servicos.Interfaces
{
    public interface IClienteAppServico
    {
        Task<string> InserirAsync(ClienteRequest request, CancellationToken cancellationToken);
        Task<string> EditarAsync(ClienteEditarRequest request, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<ClienteResponse>> ListarAsync(ClienteListarRequest request, CancellationToken cancellationToken);
        Task<ClienteResponse> RecuperarAsync(int id, CancellationToken cancellationToken);
        Task<string> AlterarStatusAsync(int id, AtivoInativoEnum status, CancellationToken cancellationToken);
    }
}
