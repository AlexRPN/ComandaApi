using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Entidades;
using Comanda.Dominio.EnderecoClientes.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.EnderecoClientes.Repositorios
{
    public class EnderecoClienteRepositorio : GenericoRepositorio<EnderecoCliente>, IEnderecoClienteRepositorio
    {
        public EnderecoClienteRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<EnderecoClienteComando> InserirAsync(EnderecoClienteComando comando, CancellationToken cancellationToken)
        {
            var enderecoCliente = new EnderecoCliente(comando);

            await appDbContext.EnderecoClientes.AddAsync(enderecoCliente, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
            comando.Id = enderecoCliente.Id;

            return comando;
        }
    }
}
