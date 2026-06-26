using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

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
    }
}
