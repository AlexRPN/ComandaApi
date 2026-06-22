using Comanda.DataTransfer.Empresas.Request;
using Comanda.DataTransfer.Empresas.Response;
using Comanda.Dominio.Empresas.Comandos;
using Mapster;

namespace Comanda.Aplicacao.Empresas.Profiles
{
    public class EmpresaProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EmpresaRequest, EmpresaComando>();
            config.NewConfig<EmpresaComando, EmpresaResponse>();            
        }
    }
}
