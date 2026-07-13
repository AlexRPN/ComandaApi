using Comanda.DataTransfer.Adicionais.Request;
using Comanda.DataTransfer.Adicionais.Response;
using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Entidades;
using Mapster;

namespace Comanda.Aplicacao.Adicionais.Profiles
{
    public class AdicionalProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdicionalRequest, AdicionalComando>();
            config.NewConfig<Adicional, AdicionalResponse>();
        }
    }
}
