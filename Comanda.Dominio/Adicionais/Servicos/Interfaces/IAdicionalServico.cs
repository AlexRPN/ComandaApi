using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Entidades;

namespace Comanda.Dominio.Adicionais.Servicos.Interfaces
{
    public interface IAdicionalServico
    {
        Task<List<Adicional>> InserirAsync(List<AdicionalComando> comando, CancellationToken cancellationToken);
    }
}
