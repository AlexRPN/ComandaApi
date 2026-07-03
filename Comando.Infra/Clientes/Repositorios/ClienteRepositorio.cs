using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Filtros;
using Comanda.Dominio.Clientes.Repositorios.Interfaces;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Comanda.Infra.Clientes.Repositorios
{
    public class ClienteRepositorio : GenericoRepositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IQueryable<Cliente>> FiltrarAsync(ClienteListarFiltro filtro, CancellationToken cancellationToken)
        {
            IQueryable<Cliente> query = appDbContext.Clientes
                .AsNoTracking()
                .Include(e => e.EnderecoCliente)
                .AsQueryable();

            if (filtro.Id.HasValue)
            {
                query = query.Where(e => e.Id == filtro.Id);
            }

            if (filtro.EmpresaId.HasValue)
            {
                query = query.Where(e => e.EmpresaId == filtro.EmpresaId);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                query = query.Where(e => e.Nome == filtro.Nome);
            }

            if (!string.IsNullOrWhiteSpace(filtro.Telefone))
            {
                query = query.Where(e => e.Telefone == filtro.Telefone);
            }

            return query;
        }

        public async Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(comando);

            await appDbContext.Clientes.AddAsync(cliente, cancellationToken);

            return cliente;
        }
    }
}
