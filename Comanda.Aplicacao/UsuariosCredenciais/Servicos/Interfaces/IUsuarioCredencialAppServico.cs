
using Comanda.DataTransfer.UsuariosCredenciais.Request;
using Comanda.DataTransfer.UsuariosCredenciais.Response;

namespace Comanda.Aplicacao.UsuariosCredenciais.Servicos.Interfaces
{
    public interface IUsuarioCredencialAppServico
    {
        Task<UsuarioCredencialRegistroResponse> RegistrarAsync(UsuarioCredencialRegistroRequest request,
                                                               CancellationToken cancellationToken);
        Task<UsuarioCredencialLoginResponse> LoginAsync(UsuarioCredencialLoginRequest request, 
                                                        CancellationToken cancellationToken);
    }
}
