
using Comanda.DataTransfer.UsuariosCredenciais.Request;
using Comanda.DataTransfer.UsuariosCredenciais.Response;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Mapster;

namespace Comanda.Aplicacao.UsuariosCredenciais.Profiles
{
    public class UsuarioCredencialProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UsuarioCredencialLoginRequest, UsuarioCredencialLoginComando>();
            config.NewConfig<UsuarioCredencialLoginComando, UsuarioCredencialLoginResponse>();
            config.NewConfig<UsuarioCredencialRegistroRequest, UsuarioCredencialComando>();
            config.NewConfig<UsuarioCredencialComando, UsuarioCredencialRegistroResponse>();
            config.NewConfig<UsuarioCredencialRegistroRequest, UsuarioInserirComando>();
            config.NewConfig<UsuarioInserirComando, UsuarioCredencialRegistroResponse>();
        }
    }
}
