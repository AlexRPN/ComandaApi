using Comanda.Dominio.Genericos;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;

namespace Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces
{
    public interface IHorarioFuncionamentoRepositorio : IGenericoRepositorio<HorarioFuncionamento>
    {
        Task<IEnumerable<HorarioFuncionamento>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken);
    }
}
