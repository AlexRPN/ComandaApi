
using Comanda.Aplicacao.HorariosFuncionamento.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.HorariosFuncionamento.Request;
using Comanda.DataTransfer.HorariosFuncionamento.Response;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.HorariosFuncionamento.Servicos
{
    public class HorarioFuncionamentoAppServico : IHorarioFuncionamentoAppServico
    {
        private readonly IHorarioFuncionamentoServico horarioFuncionamentoServico;
        private readonly ILogger<HorarioFuncionamentoAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public HorarioFuncionamentoAppServico(IHorarioFuncionamentoServico horarioFuncionamentoServico,
                                              ILogger<HorarioFuncionamentoAppServico> logger,
                                              IUnitOfWork unitOfWork)
        {
            this.horarioFuncionamentoServico = horarioFuncionamentoServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<HorarioFuncionamentoResponse>> InserirAsync(IEnumerable<HorarioFuncionamentoRequest> request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                logger.LogInformation("Iniciando inserção de horário funcionamento!");

                IEnumerable<HorarioFuncionamentoComando> comando = request.Adapt<IEnumerable<HorarioFuncionamentoComando>>();

                var listaComando = comando.ToList();

                if(listaComando.Count == 0)
                {
                    throw new ArgumentException("Nenhum horário de funcionamento informado!");
                }

                var horariosFuncionamento = await horarioFuncionamentoServico.InserirAsync(comando, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Inserção de horário funcionamento da empresa concluída com sucesso!");

                var response = horariosFuncionamento.Adapt<IEnumerable<HorarioFuncionamentoResponse>>();

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao inserir horários de funcionamento da empresa!");
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                throw new Exception($"Erro ao inserir horários de funcionamento da empresa: {ex.Message}", ex);
            }
        }


        public async Task<IEnumerable<HorarioFuncionamentoResponse>> EditarAsync(IEnumerable<HorarioFuncionamentoEditarRequest> request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                logger.LogInformation("Iniciando edição de horário funcionamento!");

                IEnumerable<HorarioFuncionamentoEditarComando> comando = request.Adapt<IEnumerable<HorarioFuncionamentoEditarComando>>();

                var horariosFuncionamento = await horarioFuncionamentoServico.EditarAsync(comando, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Edição de horário funcionamento da empresa concluída com sucesso!");

                var response = horariosFuncionamento.Adapt<IEnumerable<HorarioFuncionamentoResponse>>();

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao editar horários de funcionamento da empresa!");
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                throw new Exception($"Erro ao editar horários de funcionamento da empresa: {ex.Message}", ex);
            }
        }
    }
}
