using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.Adicionais.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.Adicionais.Repositorios
{
    public class AdicionalRepositorio : GenericoRepositorio<Adicional>, IAdicionalRepositorio
    {
        public AdicionalRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Adicional>> InserirAsync(List<AdicionalComando> comando, CancellationToken cancellationToken)
        {
            var adicionais = comando.Select(comando => new Adicional(comando)).ToList();

            await appDbContext.Adicionais.AddRangeAsync(adicionais, cancellationToken);
            return adicionais;
        }
    }
}
