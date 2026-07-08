using Comanda.Aplicacao.Categorias.Servicos.Interfaces;
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.DataTransfer.Categorias.Request;
using Comanda.DataTransfer.Categorias.Response;
using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Servicos.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.Categorias.Servicos
{
    public class CategoriaAppServico : ICategoriaAppServico
    {
        private readonly ICategoriaServico categoriaServico;
        private readonly ILogger<CategoriaAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public CategoriaAppServico(ICategoriaServico categoriaServico, 
                                   ILogger<CategoriaAppServico> logger, 
                                   IUnitOfWork unitOfWork)
        {
            this.categoriaServico = categoriaServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CategoriaResponse> InserirAsync(CategoriaRequest request, CancellationToken cancellationToken)
        {
            CategoriaComando comando = request.Adapt<CategoriaComando>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção de categoria: {Nome}", comando.Nome);
                var categoria = await categoriaServico.InserirAsync(comando, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Categoria {Nome} inserida com sucesso!", categoria.Nome);
                var response = categoria.Adapt<CategoriaResponse>();
                response.Mensagem = $"Categoria {categoria.Nome} inserida com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                logger.LogError(ex, "Erro ao inserir categoria: {Mensagem}", ex.Message);
                throw new Exception($"Erro ao inserir categoria: {ex.Message}", ex);
            }
        }

        public async Task<CategoriaResponse> EditarAsync(CategoriaEditarRequest request, CancellationToken cancellationToken)
        {
            CategoriaEditarComando comando = request.Adapt<CategoriaEditarComando>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando edição de categoria: {Nome}", comando.Nome);
                var categoria = await categoriaServico.EditarAsync(comando, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Categoria {Nome} editada com sucesso!", categoria.Nome);
                var response = categoria.Adapt<CategoriaResponse>();
                response.Mensagem = $"Categoria {categoria.Nome} editada com sucesso!";

                return response;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                logger.LogError(ex, "Erro ao editar categoria: {Mensagem}", ex.Message);
                throw new Exception($"Erro ao editar categoria: {ex.Message}", ex);
            }
        }

        public async Task<CategoriaResponse> RecuperarPorIdAsync(int id, CancellationToken cancellationToken)
        {
            Categoria categoria = await categoriaServico.RecuperarPorIdAsync(id, cancellationToken);

            var response = categoria.Adapt<CategoriaResponse>();
            response.Mensagem = $"Categoria {categoria.Nome} recuperada com sucesso!";

            return response;
        }
    }
}