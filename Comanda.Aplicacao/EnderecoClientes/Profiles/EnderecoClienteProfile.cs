using Comanda.DataTransfer.EnderecoClientes.Request;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.EnderecoClientes.Comandos;
using Mapster;

namespace Comanda.Aplicacao.EnderecoClientes.Profiles
{
    public class EnderecoClienteProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EnderecoClienteRequest, EnderecoClienteComando>();
        }
    }
}
