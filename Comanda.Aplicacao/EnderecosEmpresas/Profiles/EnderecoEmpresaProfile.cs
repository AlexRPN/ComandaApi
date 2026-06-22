
using Comanda.DataTransfer.Empresas.Response;
using Comanda.DataTransfer.EnderecosEmpresas.Request;
using Comanda.DataTransfer.EnderecosEmpresas.Response;
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Mapster;

namespace Comanda.Aplicacao.EnderecosEmpresas.Profiles
{
    public class EnderecoEmpresaProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EnderecoEmpresaRequest, EnderecoEmpresaComando>();
            config.NewConfig<EnderecoEmpresaComando, EnderecoEmpresaResponse>();
        }
    }
}
