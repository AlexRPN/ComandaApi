using Comanda.DataTransfer.Clientes.Request;
using Comanda.DataTransfer.Clientes.Response;
using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Mapster;

namespace Comanda.Aplicacao.Clientes.Profiles
{
    public class ClienteProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ClienteRequest, ClienteComando>();
            config.NewConfig<ClienteListarRequest, ClienteListarComando>();
            config.NewConfig<Cliente, ClienteResponse>();
        }
    }
}
