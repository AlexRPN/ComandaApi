using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.Aplicacao.UsuariosCredenciais.Servicos.Interfaces;
using Comanda.DataTransfer.Usuarios.Response;
using Comanda.DataTransfer.UsuariosCredenciais.Request;
using Comanda.DataTransfer.UsuariosCredenciais.Response;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Servicos.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Comanda.Aplicacao.UsuariosCredenciais.Servicos
{
    public class UsuarioCredencialAppServico : IUsuarioCredencialAppServico
    {
        private readonly IUsuarioCredencialServico usuarioCredencialServico;
        private readonly IUsuarioServico usuarioServico;
        private readonly ILogger<UsuarioCredencialAppServico> logger;
        private readonly IUnitOfWork unitOfWork;
        public UsuarioCredencialAppServico(IUsuarioCredencialServico usuarioCredencialServico,
                                           IUsuarioServico usuarioServico,
                                           ILogger<UsuarioCredencialAppServico> logger, 
                                           IUnitOfWork unitOfWork)
        {
            this.usuarioCredencialServico = usuarioCredencialServico;
            this.usuarioServico = usuarioServico;
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UsuarioCredencialRegistroResponse> RegistrarAsync(UsuarioCredencialRegistroRequest request,
                                                                            CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Iniciando registro do usuário!");
                await unitOfWork.BeginTransactionAsync(cancellationToken);

                UsuarioInserirComando usuario = request.Usuario.Adapt<UsuarioInserirComando>();

                var usuarioInserido = await usuarioServico.InserirAsync(usuario, cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                UsuarioCredencialComando comando = request.Adapt<UsuarioCredencialComando>();

                comando.UsuarioId = usuarioInserido.Id;
                var credencialRegistrada = await usuarioCredencialServico.RegistrarAsync(comando, cancellationToken);

                UsuarioCredencialRegistroResponse response = credencialRegistrada
                                                             .Adapt<UsuarioCredencialRegistroResponse>();
                response.Usuario = usuarioInserido.Adapt<UsuarioResponse>();

                await unitOfWork.CommitAsync(cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                logger.LogInformation("Registro do usuário realizado com sucesso!");
                response.Mensagem = "Usuário registrado com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                logger.LogError(ex, "Erro ao registrar usuário.");

                throw new Exception("Erro ao registrar usuário! Verifique as informações fornecidas!", ex);
            }
        }

        public async Task<UsuarioCredencialLoginResponse> LoginAsync(UsuarioCredencialLoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Iniciando Login do usuário!");

                UsuarioCredencialLoginComando comando = request.Adapt<UsuarioCredencialLoginComando>();

                var dadosLogin = await usuarioCredencialServico.LoginAsync(comando, cancellationToken);

                UsuarioCredencialLoginResponse response = dadosLogin.Adapt<UsuarioCredencialLoginResponse>();

                logger.LogInformation("Login do usuário realizado com sucesso!");
                response.Mensagem = "Login do usuário realizado com sucesso!";

                return response;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao realizar login do usuário.");
                throw new Exception("Erro ao realizar login do usuário! Verifique as credenciais informadas.", ex);
            }
        }
    }
}
