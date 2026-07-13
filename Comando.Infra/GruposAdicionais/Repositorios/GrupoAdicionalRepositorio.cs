using Comanda.Dominio.GrupoAdicionais.Comandos;
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

        public async Task<List<GrupoAdicional>> InserirAsync(List<GrupoAdicionalComando> comandos, CancellationToken cancellationToken)
        {
            var grupoAdicionais = comandos.Select(comandos => new GrupoAdicional(comandos)).ToList();

            await appDbContext.GruposAdicionais.AddRangeAsync(grupoAdicionais, cancellationToken);

            return grupoAdicionais;
        }
    }
}
