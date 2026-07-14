using Comanda.Aplicacao.GruposAdicionais.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.GruposAdicionais.Request;
using Comanda.DataTransfer.GruposAdicionais.Response;
using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Servicos.Interfaces;
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Servicos.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.GruposAdicionais.Servicos
{
    public class GrupoAdicionalAppServico : IGrupoAdicionalAppServico
    {
        private readonly IGrupoAdicionalServico grupoAdicionalServico;
        private readonly IAdicionalServico adicionalServico;
        private readonly ILogger<GrupoAdicionalAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public GrupoAdicionalAppServico(IGrupoAdicionalServico grupoAdicionalServico,
                                        IAdicionalServico adicionalServico,
                                        ILogger<GrupoAdicionalAppServico> logger,
                                        IUnitOfWork unitOfWork)
        {
            this.grupoAdicionalServico = grupoAdicionalServico;
            this.adicionalServico = adicionalServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<GrupoAdicionalResponse>> InserirAsync(List<GrupoAdicionalRequest> requests, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção de {Quantidade} grupos adicionais.", requests.Count);

                List<GrupoAdicionalComando> gruposComandos = requests.Adapt<List<GrupoAdicionalComando>>();

                var gruposInseridos = await grupoAdicionalServico.InserirAsync(gruposComandos, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);

                for (int i = 0; i < gruposInseridos.Count; i++)
                {
                    var grupoInserido = gruposInseridos[i];
                    var grupoRequest = requests[i];

                    if (grupoRequest.Adicionais is null || !grupoRequest.Adicionais.Any())
                    {
                        continue;
                    }

                    List<AdicionalComando> adicionais = grupoRequest.Adicionais .Adapt<List<AdicionalComando>>();

                    foreach (var adicional in adicionais)
                    {
                        adicional.GrupoAdicionalId = grupoInserido.Id;
                    }

                    await adicionalServico.InserirAsync( adicionais, cancellationToken);
                }

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Inserção dos grupos adicionais concluída com sucesso!");

                List<GrupoAdicionalResponse> response = gruposInseridos.Adapt<List<GrupoAdicionalResponse>>();

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao inserir grupos adicionais.");
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                throw new Exception( $"Erro ao inserir grupos adicionais: {ex.Message}", ex);
            }
        }
    }
}
