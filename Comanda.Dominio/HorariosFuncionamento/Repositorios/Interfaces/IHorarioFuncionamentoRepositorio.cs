using Comanda.Dominio.Genericos;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;

namespace Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces
{
    public interface IHorarioFuncionamentoRepositorio : IGenericoRepositorio<HorarioFuncionamento>
    {
        Task<IEnumerable<HorarioFuncionamentoComando>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken);
    }
}
