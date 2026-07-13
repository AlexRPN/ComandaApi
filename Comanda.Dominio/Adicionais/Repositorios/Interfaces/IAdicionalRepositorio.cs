using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.Genericos;

namespace Comanda.Dominio.Adicionais.Repositorios.Interfaces
{
    public interface IAdicionalRepositorio : IGenericoRepositorio<Adicional>
    {
        Task<List<Adicional>> InserirAsync(List<AdicionalComando> comando, CancellationToken cancellationToken);
    }
}
