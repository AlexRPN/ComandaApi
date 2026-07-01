using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Entidades;
using Comanda.Dominio.Genericos;

namespace Comanda.Dominio.EnderecoClientes.Repositorios.Interfaces
{
    public interface IEnderecoClienteRepositorio : IGenericoRepositorio<EnderecoCliente>
    {
        Task<EnderecoClienteComando> InserirAsync(EnderecoClienteComando comando, CancellationToken cancellationToken);
    }
}
