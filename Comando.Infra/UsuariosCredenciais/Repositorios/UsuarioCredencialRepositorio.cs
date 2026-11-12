
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Entidades;
using Comanda.Dominio.UsuariosCredenciais.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.UsuariosCredenciais.Repositorios
{
    public class UsuarioCredencialRepositorio : GenericoRepositorio<UsuarioCredencial>, IUsuarioCredencialRepositorio
    {
        public UsuarioCredencialRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<UsuarioCredencial> RegistrarAsync(UsuarioCredencialComando comando, 
                                                            CancellationToken cancellationToken)
        {
            UsuarioCredencial usuarioCredencial = new UsuarioCredencial(comando);

            await appDbContext.UsuariosCredenciais.AddAsync(usuarioCredencial, cancellationToken);

            return usuarioCredencial;
        }

        public Task<UsuarioCredencial?> RecuperarPorNomeUsuarioAsync(string nomeUsuario, 
                                                                     CancellationToken cancellationToken)
        {
            return appDbContext.UsuariosCredenciais
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.NomeUsuario == nomeUsuario, cancellationToken);
        }
    }
}
