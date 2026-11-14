
using Comanda.DataTransfer.Mesas.Request;
using Comanda.Dominio.Mesas.Comandos;
using Mapster;

namespace Comanda.Aplicacao.Mesas.Profiles
{
    public class MesaProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MesaRequest, MesaComando>();
        }
    }
}
