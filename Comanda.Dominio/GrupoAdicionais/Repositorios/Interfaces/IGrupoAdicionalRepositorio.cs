using Comanda.Dominio.Genericos;
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Entidades;

namespace Comanda.Dominio.GrupoAdicionais.Repositorios.Interfaces
{
    public interface IGrupoAdicionalRepositorio : IGenericoRepositorio<GrupoAdicional>
    {
        Task<List<GrupoAdicional>> InserirAsync(List<GrupoAdicionalComando> comandos, CancellationToken cancellationToken);
    }
}
