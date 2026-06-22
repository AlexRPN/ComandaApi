using Comanda.DataTransfer.Empresas.Response;
using Comanda.DataTransfer.HorariosFuncionamento.Request;
using Comanda.DataTransfer.HorariosFuncionamento.Response;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Mapster;

namespace Comanda.Aplicacao.HorariosFuncionamento.Profiles
{
    public class HorarioFuncionamentoProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<HorarioFuncionamentoRequest, HorarioFuncionamentoComando>();
            config.NewConfig<HorarioFuncionamentoComando, HorarioFuncionamentoResponse>();
        }
    }
}
