
using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.Aplicacao.Usuarios.Servicos.Interfaces;
using Comanda.DataTransfer.Empresas.Response;
using Comanda.DataTransfer.EnderecosEmpresas.Response;
using Comanda.DataTransfer.HorariosFuncionamento.Response;
using Comanda.DataTransfer.Usuarios.Request;
using Comanda.DataTransfer.Usuarios.Response;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Filtros;
using Comanda.Dominio.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.Utils.Consultas;
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

        public async Task<string> InserirAsync(UsuarioRequest request, CancellationToken cancellationToken)
        {
            UsuarioInserirComando comando = request.Adapt<UsuarioInserirComando>();
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                logger.LogInformation("Iniciando inserção de usuário.");
                await empresaServico.ValidarAsync(comando.EmpresaId, cancellationToken);

                await usuarioServico.InserirAsync(comando, cancellationToken);

                var mensagem = $"Usuário {comando.Nome} inserido com sucesso.";

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                return mensagem;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);

                logger.LogError(ex, "Erro ao inserir usuário.");
                throw new Exception("Erro ao inserir usuário.", ex);
            }
        }

        public async Task<string> EditarAsync(UsuarioEditarRequest request, CancellationToken cancellationToken)
        {
            UsuarioEditarComando comando = request.Adapt<UsuarioEditarComando>();

            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                logger.LogInformation("Iniciando edição de cadastro do usuário {Nome}", request.Nome);

                await usuarioServico.RecuperarPorIdAsync(request.Id, cancellationToken);

                await usuarioServico.EditarAsync(comando, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var mensagem = $"Dados do usuário {request.Nome} atualizados com sucesso!";

                return mensagem;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao editar usuário {Nome}", request.Nome);
                throw new Exception("Erro ao editar usuário!", ex);
            }
        }

        public async Task<PaginacaoConsulta<UsuarioResponse>> ListarPaginadoAsync(UsuarioListarRequest request, CancellationToken cancellationToken)
        {
            UsuarioListarFiltro filtro = request.Adapt<UsuarioListarFiltro>();

            IQueryable<Usuario> query = await usuarioServico.FiltrarAsync(filtro, cancellationToken);

            PaginacaoConsulta<Usuario> usuarios = await usuarioServico.ListarPaginadoAsync(query, request.Qt, request.Pg, request.CpOrd, request.TpOrd, cancellationToken);

            PaginacaoConsulta<UsuarioResponse> response = usuarios.Adapt<PaginacaoConsulta<UsuarioResponse>>();

            return response;
        }

        public async Task<UsuarioResponse> RecuperarPorIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await usuarioServico.RecuperarPorIdAsync(id, cancellationToken);
                var response = usuario.Adapt<UsuarioResponse>();

                response.Empresa = usuario.Empresa.Adapt<EmpresaResponse>();
                response.Empresa.Endereco = usuario.Empresa.EnderecoEmpresa.Adapt<EnderecoEmpresaResponse>();
                response.Empresa.HorariosFuncionamento = usuario.Empresa.HorariosFuncionamento.Adapt<IEnumerable<HorarioFuncionamentoResponse>>();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar usuário!", ex);
            }
        }

        public async Task<string> AlterarStatusAsync(int id, StatusEnum status, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                var statusAlterado = await usuarioServico.AlterarStatusAsync(id, status, cancellationToken);

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                var mensagem = $"Status do usuário {statusAlterado.Nome} alterado com sucesso!";
                return mensagem;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                logger.LogError(ex, "Erro ao alterar status do usuário {Id}", id);
                throw new Exception("Erro ao alterar status do usuário!", ex);
            }
        }
    }
}
