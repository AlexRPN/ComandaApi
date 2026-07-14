using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.GrupoAdicionais.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.GruposAdicionais.Repositorios
{
    public class GrupoAdicionalRepositorio : GenericoRepositorio<GrupoAdicional>, IGrupoAdicionalRepositorio
    {
        public GrupoAdicionalRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<GrupoAdicional>> InserirAsync(List<GrupoAdicional> gruposAdicionais, CancellationToken cancellationToken)
        {
            await appDbContext.GruposAdicionais.AddRangeAsync(gruposAdicionais, cancellationToken);

            return gruposAdicionais;
        }
    }
}
