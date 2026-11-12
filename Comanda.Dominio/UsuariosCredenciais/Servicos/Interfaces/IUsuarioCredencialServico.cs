
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Entidades;

namespace Comanda.Dominio.UsuariosCredenciais.Servicos.Interfaces
{
    public interface IUsuarioCredencialServico
    {
        Task<UsuarioCredencialRegistroComando> RegistrarAsync(UsuarioCredencialComando comando, 
                                                              CancellationToken cancellationToken);
        Task<UsuarioCredencialComando> LoginAsync(UsuarioCredencialLoginComando comando, 
                                                  CancellationToken cancellationToken);
    }
}
