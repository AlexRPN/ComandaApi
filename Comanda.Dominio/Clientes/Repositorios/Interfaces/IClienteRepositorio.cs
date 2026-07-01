using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Genericos;

namespace Comanda.Dominio.Clientes.Repositorios.Interfaces
{
    public interface IClienteRepositorio : IGenericoRepositorio<Cliente>
    {
        Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken);
    }
}
