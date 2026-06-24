using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;

namespace Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces
{
    public interface IHorarioFuncionamentoServico
    {
        Task<IEnumerable<HorarioFuncionamentoComando>> InserirAsync(IEnumerable<HorarioFuncionamentoInserirComando> comando, CancellationToken cancellationToken);
        Task<IEnumerable<HorarioFuncionamento>> EditarAsync(int empresaId, IEnumerable<HorarioFuncionamentoEditarComando> comando, CancellationToken cancellationToken);
        Task<IEnumerable<HorarioFuncionamento>> ValidarAsync(int id, CancellationToken cancellationToken);
    }
}
