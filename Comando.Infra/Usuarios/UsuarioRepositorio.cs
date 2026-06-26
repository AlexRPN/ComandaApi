using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Interfaces;
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
    }
}
