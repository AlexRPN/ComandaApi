using Comanda.DataTransfer.Empresas.Request;
using Comanda.DataTransfer.Empresas.Response;
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Mapster;

namespace Comanda.Aplicacao.Empresas.Profiles
{
    public class EmpresaProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EmpresaRequest, EmpresaComando>();
            config.NewConfig<EmpresaComando, EmpresaResponse>();
            config.NewConfig<Empresa, EmpresaResponse>()
                .Map(dest => dest.Endereco, src => src.EnderecoEmpresa)
                .Map(dest => dest.HorariosFuncionamento, src => src.HorariosFuncionamento);
            config.NewConfig<EmpresaListarRequest, EmpresaListarFiltro>();
        }
    }
}
