using Comanda.Dominio.HorariosFuncionamento.Comando;

namespace Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces
{
    public interface IHorarioFuncionamentoRepositorio
    {
        Task<IEnumerable<HorarioFuncionamentoComando>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken);
    }
}
