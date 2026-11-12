
using Comanda.Dominio.Genericos;
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Entidades;

namespace Comanda.Dominio.UsuariosCredenciais.Repositorios.Interfaces
{
    public interface IUsuarioCredencialRepositorio : IGenericoRepositorio<UsuarioCredencial>
    {
        Task<UsuarioCredencial> RegistrarAsync(UsuarioCredencialComando comando, CancellationToken cancellationToken);
        Task<UsuarioCredencial?> RecuperarPorNomeUsuarioAsync(string nomeUsuario,
                                                              CancellationToken cancellationToken);
    }
}
