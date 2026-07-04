using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Entidades;

namespace Comanda.Dominio.EnderecoClientes.Servicos.Interfaces
{
    public interface IEnderecoClienteServico
    {
        Task<EnderecoClienteComando> InserirAsync(EnderecoClienteComando comando, CancellationToken cancellationToken);
        Task<EnderecoCliente> EditarAsync(EnderecoClienteEditarComando comando, CancellationToken cancellationToken);
    }
}
