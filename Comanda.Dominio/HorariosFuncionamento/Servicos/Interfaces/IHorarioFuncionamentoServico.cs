using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;

namespace Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces
{
    public interface IHorarioFuncionamentoServico
    {
        Task<IEnumerable<HorarioFuncionamento>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken);
        Task<IEnumerable<HorarioFuncionamento>> EditarAsync(IEnumerable<HorarioFuncionamentoEditarComando> comando, CancellationToken cancellationToken);
        Task<IEnumerable<HorarioFuncionamento>> RecuperarAsync(int idEmpresa, CancellationToken cancellationToken);
    }
}
