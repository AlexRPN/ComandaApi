
using Comanda.Aplicacao.Mesas.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.Mesas.Request;
using Comanda.DataTransfer.Mesas.Response;
using Comanda.Dominio.Mesas.Comandos;
using Comanda.Dominio.Mesas.Servicos.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.Mesas.Servicos
{
    public class MesaAppServico : IMesaAppServico
    {
        private readonly IMesaServico mesaServico;
        private readonly ILogger<MesaAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public MesaAppServico(IMesaServico mesaServico, 
                              ILogger<MesaAppServico> logger, 
                              IUnitOfWork unitOfWork)
        {
            this.mesaServico = mesaServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> InserirAsync(MesaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                logger.LogInformation("Iniciando inserção de mesa.");

                MesaComando comando = request.Adapt<MesaComando>();

                await mesaServico.InserirAsync(comando, cancellationToken);

                logger.LogInformation("Mesa inserida com sucesso.");
                var response = $"Mesa {comando.Numero} inserida com sucesso!";

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                return response;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao inserir mesa.");
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                throw new Exception("Erro ao inserir mesa.", ex);
            }
        }

        public async Task<MesaResponse> EditarAsync(MesaEditarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                logger.LogInformation("Iniciando edição de mesa!");

                MesaEditarComando comando = request.Adapt<MesaEditarComando>();

                var mesaEditada = await mesaServico.EditarAsync(comando, cancellationToken);

                MesaResponse response = mesaEditada.Adapt<MesaResponse>();

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Mesa editada com sucesso!");
                response.Mensagem = $"Mesa {response.Numero} editada com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao editar mesa.");
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                throw new Exception("Erro ao editar mesa.", ex);
            }
        }
    }
}
