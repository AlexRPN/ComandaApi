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
        private readonly ILogger<ProdutoAppServico> logger;
        private readonly IUnitOfWork unitOfWork;

        public ProdutoAppServico(IProdutoServico produtoServico,
                                 IProdutoVariacaoServico produtoVariacaoServico,
                                 IImagemProdutoServico imagemProdutoServico,
                                 IGrupoAdicionalServico grupoAdicionalServico,
                                 IAdicionalServico adicionalServico,
                                 ILogger<ProdutoAppServico> logger,
                                 IUnitOfWork unitOfWork)
        {
            this.produtoServico = produtoServico;
            this.produtoVariacaoServico = produtoVariacaoServico;
            this.imagemProdutoServico = imagemProdutoServico;
            this.grupoAdicionalServico = grupoAdicionalServico;
            this.adicionalServico = adicionalServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProdutoResponse> InserirAsync(ProdutoRequest request, CancellationToken cancellationToken)
        {
            ProdutoComando produtoComando = request.Adapt<ProdutoComando>();
            List<ProdutoVariacaoComando> produtoVariacoes = request.ProdutoVariacao.Adapt<List<ProdutoVariacaoComando>>();
            //List<ImagemProdutoComando>? imagens = request.Imagens.Adapt<List<ImagemProdutoComando>>();
            List<GrupoAdicionalComando>? gruposAdicionais = request.GruposAdicionais.Adapt<List<GrupoAdicionalComando>>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção de produtos.");
                var produtoInserido = await produtoServico.InserirAsync(produtoComando, cancellationToken);

                if (produtoInserido == null)
                {
                    throw new Exception("Erro ao inserir produto.");
                }

                await unitOfWork.CommitAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção de variações de produtos.");
                foreach (var variacao in produtoVariacoes)
                {
                    variacao.ProdutoId = produtoInserido.Id;
                }

                await produtoVariacaoServico.InserirAsync(produtoVariacoes, cancellationToken);

                #region Implementar a lógica de upload de imagens e inserção no banco de dados quando o serviço de upload estiver disponível.
                //logger.LogInformation("Iniciando inserção de imagens de produtos.");
                //foreach (var arquivo in request.Imagens)
                //{
                //    string caminho = await uploadService.UploadAsync(arquivo);

                //    imagens.Add(new ImagemProdutoComando
                //    {
                //        ProdutoId = produtoInserido.Id,
                //        CaminhoArquivo = caminho,
                //        UrlImagem = caminho
                //    });
                //}
                //await imagemProdutoServico.InserirAsync(imagens, cancellationToken);
                #endregion

                logger.LogInformation("Iniciando inserção de grupo adicional de produtos.");
                foreach (var grupo in gruposAdicionais)
                {
                    grupo.ProdutoId = produtoInserido.Id;
                }
                var grupoAdicionalInserido = await grupoAdicionalServico.InserirAsync(gruposAdicionais, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);

                if (grupoAdicionalInserido != null && grupoAdicionalInserido.Any())
                {
                    logger.LogInformation("Iniciando inserção de adicionais de produtos.");

                    for (int i = 0; i < grupoAdicionalInserido.Count; i++)
                    {
                        var grupoInserido = grupoAdicionalInserido[i];
                        var grupoRequest = request.GruposAdicionais[i];

                        List<AdicionalComando> adicionaisDoGrupo = grupoRequest.Adicionais.Adapt<List<AdicionalComando>>();

                        foreach (var adicional in adicionaisDoGrupo)
                        {
                            adicional.GrupoAdicionalId = grupoInserido.Id;
                        }

                        await adicionalServico.InserirAsync(adicionaisDoGrupo, cancellationToken);
                    }
                }

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Inserção de produtos concluída com sucesso!");

                ProdutoResponse response = produtoInserido.Adapt<ProdutoResponse>();
                //response.ImagensProdutos = imagens.Adapt<List<ImagemProdutoResponse>>() ?? [];
                response.ProdutoVariacao = produtoVariacoes.Adapt<List<ProdutoVariacaoResponse>>() ?? [];
                response.GrupoAdicional = gruposAdicionais.Adapt<List<GrupoAdicionalResponse>>() ?? [];
                response.GrupoAdicional = grupoAdicionalInserido.Adapt<List<GrupoAdicionalResponse>>() ?? [];

                response.Mensagem = $"Produto {produtoInserido.Nome} inserido com sucesso.";

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao inserir produto.");
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw new Exception($"Erro ao inserir produto: {ex.Message}");
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
