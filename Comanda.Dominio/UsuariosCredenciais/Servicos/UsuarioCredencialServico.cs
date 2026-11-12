
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Repositorios.Interfaces;
using Comanda.Dominio.UsuariosCredenciais.Servicos.Interfaces;
using Comanda.Dominio.Utils.Autenticacoes.Servicos.Interfaces;

namespace Comanda.Dominio.UsuariosCredenciais.Servicos
{
    public class UsuarioCredencialServico : IUsuarioCredencialServico
    {
        private readonly IUsuarioCredencialRepositorio usuarioCredencialRepositorio;
        private readonly IAutenticacaoServico autenticacaoServico;
        public UsuarioCredencialServico(IUsuarioCredencialRepositorio usuarioCredencialRepositorio,
                                        IAutenticacaoServico autenticacaoServico)
        {
            this.usuarioCredencialRepositorio = usuarioCredencialRepositorio;
            this.autenticacaoServico = autenticacaoServico;
        }

        public async Task<UsuarioCredencialRegistroComando> RegistrarAsync(UsuarioCredencialComando comando,
                                                                           CancellationToken cancellationToken)
        {
            var usuarioRegistrado = new UsuarioCredencialRegistroComando();

            var credencial = await usuarioCredencialRepositorio.RecuperarAsync(x => x.UsuarioId == comando.UsuarioId ||
                                                                               x.NomeUsuario == comando.NomeUsuario, 
                                                                               cancellationToken);

            if(credencial != null)
            {
                throw new Exception("O nome de usuário informado já está em uso!");
            }

            autenticacaoServico.CriarSenhaHash(comando.Senha, out var senhaHash, out var senhaSalt);

            comando.SenhaHash = senhaHash;
            comando.SenhaSalt = senhaSalt;

            var usuarioCredencial = await usuarioCredencialRepositorio.RegistrarAsync(comando, cancellationToken);

            usuarioRegistrado.UsuarioId = usuarioCredencial.UsuarioId;
            usuarioRegistrado.NomeUsuario = usuarioCredencial.NomeUsuario;
            usuarioRegistrado.Mensagem = "Usuário registrado com sucesso!";

            return usuarioRegistrado;
        }

        public async Task<UsuarioCredencialComando> LoginAsync(UsuarioCredencialLoginComando comando, CancellationToken cancellationToken)
        {
            var usuarioCredencial = await usuarioCredencialRepositorio
                .RecuperarPorNomeUsuarioAsync(comando.NomeUsuario, cancellationToken);

            if (usuarioCredencial is null)
            {
                throw new ArgumentException("O nome de usuário informado não foi encontrado!");
            }

            if (!autenticacaoServico.VerificarLogin(comando.Senha,
                                                    usuarioCredencial.SenhaHash, 
                                                    usuarioCredencial.SenhaSalt))
            {
                throw new ArgumentException("A senha informada está incorreta!");
            }

            var token = autenticacaoServico.CriarTokenJwt(usuarioCredencial, cancellationToken);

            var dadosLogin = new UsuarioCredencialComando()
            {
                NomeUsuario = usuarioCredencial.NomeUsuario,
                DataAcesso = DateTime.Now,
                Token = token
            };

            return dadosLogin;
        }
    }
}
