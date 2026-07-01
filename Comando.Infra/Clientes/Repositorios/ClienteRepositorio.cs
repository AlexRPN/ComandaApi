using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.Clientes.Repositorios
{
    public class ClienteRepositorio : GenericoRepositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(comando);

            await appDbContext.Clientes.AddAsync(cliente, cancellationToken);

            return cliente;
        }
    }
}
