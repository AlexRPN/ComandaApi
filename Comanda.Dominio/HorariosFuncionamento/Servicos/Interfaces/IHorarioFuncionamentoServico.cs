using Comanda.Dominio.HorariosFuncionamento.Comando;

namespace Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces
{
    public interface IHorarioFuncionamentoServico
    {
        Task<IEnumerable<HorarioFuncionamentoComando>> InserirAsync(IEnumerable<HorarioFuncionamentoInserirComando> comando, CancellationToken cancellationToken);
    }
}
