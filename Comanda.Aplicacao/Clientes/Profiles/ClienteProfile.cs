using Comanda.DataTransfer.Clientes.Request;
using Comanda.Dominio.Clientes.Comandos;
using Mapster;

namespace Comanda.Aplicacao.Clientes.Profiles
{
    public class ClienteProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ClienteRequest, ClienteComando>();
        }
    }
}
