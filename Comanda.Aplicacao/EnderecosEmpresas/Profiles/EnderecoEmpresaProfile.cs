using Comanda.DataTransfer.Empresas.Request;
using Comanda.DataTransfer.Empresas.Response;
using Comanda.DataTransfer.EnderecosEmpresas.Request;
using Comanda.DataTransfer.EnderecosEmpresas.Response;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Mapster;

namespace Comanda.Aplicacao.EnderecosEmpresas.Profiles
{
    public class EnderecoEmpresaProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EnderecoEmpresaRequest, EnderecoEmpresaComando>();
            config.NewConfig<EnderecoEmpresaComando, EnderecoEmpresaResponse>();
            config.NewConfig<Empresa, EnderecoEmpresaResponse>();
            config.NewConfig<EmpresaRequest, EnderecoEmpresaEditarComando>();
        }
    }
}
