
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.Aplicacao.Usuarios.Servicos.Interfaces;
using Comanda.DataTransfer.Usuarios.Request;
using Comanda.DataTransfer.Usuarios.Response;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.Usuarios.Servicos
{
    public class UsuarioAppServico : IUsuarioAppServico
    {
        private readonly IUsuarioServico usuarioServico;
        private readonly IEmpresaServico empresaServico;
        private readonly ILogger<UsuarioAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public UsuarioAppServico(IUsuarioServico usuarioServico, 
                                 IEmpresaServico empresaServico, 
                                 ILogger<UsuarioAppServico> logger, 
                                 IUnitOfWork unitOfWork)
        {
            this.usuarioServico = usuarioServico;
            this.empresaServico = empresaServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UsuarioResponse> InserirAsync(UsuarioRequest request, CancellationToken cancellationToken)
        {
            UsuarioInserirComando comando = request.Adapt<UsuarioInserirComando>();
            UsuarioResponse response = new UsuarioResponse();
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção de usuário.");
                var empresaValida = await empresaServico.ValidarAsync(comando.EmpresaId, cancellationToken);

                if(empresaValida == null || (empresaValida.Status == AtivoInativoEnum.Inativo))
                {
                    throw new Exception("Empresa inválida ou inativa.");
                }

                await usuarioServico.InserirAsync(comando, cancellationToken);

                response.Mensagem = $"Usuário {comando.Nome} inserido com sucesso.";

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Erro ao inserir usuário.");
                throw new Exception("Erro ao inserir usuário.", ex);
            }
        }
    }
}
