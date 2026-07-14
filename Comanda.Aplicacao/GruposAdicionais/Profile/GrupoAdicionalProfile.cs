using Comanda.DataTransfer.GruposAdicionais.Request;
using Comanda.DataTransfer.GruposAdicionais.Response;
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using Mapster;

namespace Comanda.Aplicacao.GruposAdicionais.Profile
{
    public class GrupoAdicionalProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<List<GrupoAdicionalRequest>, List<GrupoAdicionalComando>>()
                .Map(dest => dest, src => src.Select(x => x.Adapt<GrupoAdicionalComando>()).ToList());
            config.NewConfig<GrupoAdicional, GrupoAdicionalResponse>();
        }
    }
}
