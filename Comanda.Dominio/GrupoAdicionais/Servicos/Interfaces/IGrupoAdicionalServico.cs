using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Entidades;

namespace Comanda.Dominio.GrupoAdicionais.Servicos.Interfaces
{
    public interface IGrupoAdicionalServico
    {
        Task<List<GrupoAdicional>> InserirAsync(List<GrupoAdicionalComando> comandos, CancellationToken cancellationToken);
        Task<GrupoAdicional> RecuperarAsync(int empresaId, int grupoAdicionalId, CancellationToken cancellationToken);
    }
}
