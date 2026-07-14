using Comanda.Aplicacao.Produtos.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.Adicionais.Response;
using Comanda.DataTransfer.GruposAdicionais.Response;
using Comanda.DataTransfer.Produtos.Request;
using Comanda.DataTransfer.Produtos.Response;
using Comanda.DataTransfer.ProdutosVariacoes.Response;
using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Servicos.Interfaces;
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Servicos.Interfaces;
using Comanda.Dominio.ImagensProdutos.Servicos.Interfaces;
using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Repositorios.Filtros;
using Comanda.Dominio.Produtos.Servicos.Interfaces;
using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;
using Comanda.Dominio.ProdutosGruposAdicionais.Servicos;
using Comanda.Dominio.ProdutosGruposAdicionais.Servicos.Interfaces;
using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Services.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.Produtos.Servicos
{
    public class ProdutoAppServico : IProdutoAppServico
    {
        private readonly IProdutoServico produtoServico;
        private readonly IProdutoVariacaoServico produtoVariacaoServico;
        private readonly IImagemProdutoServico imagemProdutoServico;
        private readonly IGrupoAdicionalServico grupoAdicionalServico;
        private readonly IAdicionalServico adicionalServico;
        private readonly IProdutoGrupoAdicionalServico produtoGrupoAdicionalServico;
        private readonly ILogger<ProdutoAppServico> logger;
        private readonly IUnitOfWork unitOfWork;

        public ProdutoAppServico(IProdutoServico produtoServico,
                                 IProdutoVariacaoServico produtoVariacaoServico,
                                 IImagemProdutoServico imagemProdutoServico,
                                 IGrupoAdicionalServico grupoAdicionalServico,
                                 IAdicionalServico adicionalServico,
                                 IProdutoGrupoAdicionalServico produtoGrupoAdicionalServico,
                                 ILogger<ProdutoAppServico> logger,
                                 IUnitOfWork unitOfWork)
        {
            this.produtoServico = produtoServico;
            this.produtoVariacaoServico = produtoVariacaoServico;
            this.imagemProdutoServico = imagemProdutoServico;
            this.grupoAdicionalServico = grupoAdicionalServico;
            this.adicionalServico = adicionalServico;
            this.produtoGrupoAdicionalServico = produtoGrupoAdicionalServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProdutoResponse> InserirAsync(ProdutoRequest request, CancellationToken cancellationToken)
        {
            ProdutoComando produtoComando = request.Adapt<ProdutoComando>();
            List<ProdutoVariacaoComando> produtoVariacoes = request.ProdutoVariacao.Adapt<List<ProdutoVariacaoComando>>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção do produto.");

                var produtoInserido = await produtoServico.InserirAsync(produtoComando, cancellationToken);

                if (produtoInserido is null)
                {
                    throw new Exception("Erro ao inserir produto.");
                }

                // Implementar a lógica para salvar as imagens do produto quando o serviço de imagens estiver pronto

                // Necessário para obter o Id gerado
                await unitOfWork.CommitAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção das variações do produto.");

                foreach (var variacao in produtoVariacoes)
                {
                    variacao.ProdutoId = produtoInserido.Id;
                }

                await produtoVariacaoServico.InserirAsync(produtoVariacoes, cancellationToken);

                if (request.GruposAdicionaisIds is not null && request.GruposAdicionaisIds.Any())
                {
                    logger.LogInformation("Iniciando vínculo dos grupos adicionais ao produto.");

                    foreach (int grupoAdicionalId in request.GruposAdicionaisIds)
                    {
                        var grupo = await grupoAdicionalServico.RecuperarAsync(request.EmpresaId, grupoAdicionalId, cancellationToken);

                        ProdutoGrupoAdicionalComando comando = new()
                        {
                            ProdutoId = produtoInserido.Id,
                            GrupoAdicionalId = grupo.Id
                        };

                        await produtoGrupoAdicionalServico.InserirAsync(comando, cancellationToken);
                    }
                }

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Inserção do produto concluída com sucesso.");

                ProdutoResponse response = produtoInserido.Adapt<ProdutoResponse>();
                response.ProdutoVariacao = produtoVariacoes.Adapt<List<ProdutoVariacaoResponse>>();
                response.Mensagem = $"Produto {produtoInserido.Nome} inserido com sucesso.";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao inserir produto.");

                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                throw new Exception($"Erro ao inserir produto: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<ProdutoListarResponse>> ListarAsync(ProdutoFiltroRequest request, CancellationToken cancellationToken)
        {
            ProdutoListarFiltro filtro = request.Adapt<ProdutoListarFiltro>();

            var produtos = await produtoServico.ListarAsync(filtro, cancellationToken);

            ProdutoListarResponse[] response = produtos.Adapt<ProdutoListarResponse[]>();
            return response;
        }
    }
}
