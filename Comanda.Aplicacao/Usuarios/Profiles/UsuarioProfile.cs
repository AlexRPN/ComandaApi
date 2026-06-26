
using Comanda.DataTransfer.Usuarios.Request;
using Comanda.DataTransfer.Usuarios.Response;
using Comanda.Dominio.Usuarios.Comandos;
using Mapster;

namespace Comanda.Aplicacao.Usuarios.Profiles
{
    public class UsuarioProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UsuarioRequest, UsuarioInserirComando>();
        }
    }
}
