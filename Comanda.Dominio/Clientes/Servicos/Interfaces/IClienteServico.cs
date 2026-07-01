using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;

namespace Comanda.Dominio.Clientes.Servicos.Interfaces
{
    public interface IClienteServico
    {
        Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken);
        Task<Cliente> RecuperarAsync(int id, CancellationToken cancellationToken);
    }
}
