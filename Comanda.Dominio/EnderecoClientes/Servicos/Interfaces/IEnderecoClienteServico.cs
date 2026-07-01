using Comanda.Dominio.EnderecoClientes.Comandos;

namespace Comanda.Dominio.EnderecoClientes.Servicos.Interfaces
{
    public interface IEnderecoClienteServico
    {
        Task<EnderecoClienteComando> InserirAsync(EnderecoClienteComando comando, CancellationToken cancellationToken);
    }
}
