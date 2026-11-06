
using Comanda.DataTransfer.HorariosFuncionamento.Request;
using Comanda.DataTransfer.HorariosFuncionamento.Response;

namespace Comanda.Aplicacao.HorariosFuncionamento.Servicos.Interfaces
{
    public interface IHorarioFuncionamentoAppServico
    {
        Task<IEnumerable<HorarioFuncionamentoResponse>> InserirAsync(IEnumerable<HorarioFuncionamentoRequest> request,
                                                                      CancellationToken cancellationToken);
        Task<IEnumerable<HorarioFuncionamentoResponse>> EditarAsync(IEnumerable<HorarioFuncionamentoEditarRequest> request, 
                                                                    CancellationToken cancellationToken);
    }
}
