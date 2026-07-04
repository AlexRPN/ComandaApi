using Comanda.DataTransfer.Clientes.Request;
using Comanda.DataTransfer.Clientes.Response;
using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Filtros;
using Mapster;

namespace Comanda.Aplicacao.Clientes.Profiles
{
    public class ClienteProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ClienteRequest, ClienteComando>();
            config.NewConfig<ClienteListarRequest, ClienteListarFiltro>();
            config.NewConfig<Cliente, ClienteResponse>();
            config.NewConfig<ClienteEditarRequest, ClienteEditarComando>();
        }
    }
}
