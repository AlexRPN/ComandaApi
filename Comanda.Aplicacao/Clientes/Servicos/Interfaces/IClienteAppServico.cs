using Comanda.DataTransfer.Clientes.Request;

namespace Comanda.Aplicacao.Clientes.Servicos.Interfaces
{
    public interface IClienteAppServico
    {
        Task<string> InserirAsync(ClienteRequest request, CancellationToken cancellationToken);
    }
}
