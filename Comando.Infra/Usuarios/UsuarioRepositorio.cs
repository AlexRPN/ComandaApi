using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Filtros;
using Comanda.Dominio.Usuarios.Repositorios.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Usuarios
{
    public class UsuarioRepositorio : GenericoRepositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Usuario> InserirAsync(UsuarioComando comando, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(comando);

            await appDbContext.Usuarios.AddAsync(usuario, cancellationToken);
            comando.Id = usuario.Id;

            return usuario;
        }

        public Task<Usuario> RecuperarPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var usuario = appDbContext.Usuarios
                .AsNoTracking()
                .Include(e => e.Empresa)
                .ThenInclude(e => e.EnderecoEmpresa)
                .Include(u => u.Empresa.HorariosFuncionamento)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return usuario;
        }

        public async Task<Usuario> ValidarCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            var usuario = await appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Cpf == cpf, cancellationToken);

            return usuario;
        }

        public async Task<IQueryable<Usuario>> FiltrarAsync(UsuarioListarFiltro comando, CancellationToken cancellationToken)
        {
            IQueryable<Usuario> query = appDbContext.Usuarios
                    .AsNoTracking()
                    .Include(e => e.Empresa)
                    .ThenInclude(e => e.EnderecoEmpresa)
                    .Include(u => u.Empresa.HorariosFuncionamento)
                    .AsQueryable();

            if (comando.Id.HasValue)
            {
                query = query.Where(u => u.Id == comando.Id.Value);
            }

            if (!string.IsNullOrEmpty(comando.Cpf))
            {
                query = query.Where(u => u.Cpf == comando.Cpf);
            }

            return query;
        }

        public async Task<PaginacaoConsulta<Usuario>> ListarPaginadoAsync(IQueryable<Usuario> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken)
        {
            return await base.ListarAsync(query, qt, pg, cpOrd, tpOrd, cancellationToken);
        }
    }
}
