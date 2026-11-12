using System.Linq.Expressions;
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Entidades;
using Comanda.Dominio.UsuariosCredenciais.Repositorios.Interfaces;
using Comanda.Dominio.UsuariosCredenciais.Servicos;
using Comanda.Dominio.Utils.Autenticacoes.Servicos.Interfaces;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;

namespace Comanda.Dominio.Testes.UsuariosCredenciais.Servicos
{
    public class UsuariosCredenciaisServicoTestes
    {
        protected readonly UsuarioCredencialServico sut;
        protected readonly UsuarioCredencialComando usuarioCredencialComando;
        protected readonly IUsuarioCredencialRepositorio usuarioCredencialRepositorioMock;
        protected readonly IAutenticacaoServico autenticacaoServico;
        protected readonly CancellationToken cancellationToken = CancellationToken.None;

        public UsuariosCredenciaisServicoTestes()
        {
            usuarioCredencialRepositorioMock = Substitute.For<IUsuarioCredencialRepositorio>();
            autenticacaoServico = Substitute.For<IAutenticacaoServico>();
            usuarioCredencialComando = Builder<UsuarioCredencialComando>
                .CreateNew()
                .With(x => x.UsuarioId = 1)
                .With(x => x.NomeUsuario = "teste.teste")
                .With(x => x.Senha = "12345")
                .With(x => x.SenhaHash = new byte[] { 1, 2, 3 })
                .With(x => x.SenhaSalt = new byte[] { 4, 5, 6 })
                .With(x => x.DataAlteracaoSenha = DateTime.Now)
                .With(x => x.UltimoAcesso = DateTime.Now)
                .With(x => x.DataAcesso = DateTime.Now)
                .With(x => x.DataFimBloqueio = null)
                .With(x => x.Token = "token123")
                .Build();
            sut = new UsuarioCredencialServico(usuarioCredencialRepositorioMock, autenticacaoServico);
        }

        protected static UsuarioCredencial CriarCredencial(int usuarioId, string nomeUsuario)
        {
            return new UsuarioCredencial(new UsuarioCredencialComando
            {
                UsuarioId = usuarioId,
                NomeUsuario = nomeUsuario,
                SenhaHash = new byte[] { 1, 2, 3 },
                SenhaSalt = new byte[] { 4, 5, 6 },
                DataAlteracaoSenha = DateTime.UtcNow,
                UltimoAcesso = DateTime.UtcNow
            });
        }

        public class RegistrarAsyncMetodo : UsuariosCredenciaisServicoTestes
        {
            [Fact]
            public async Task Quando_DadosDeRegistroForemValidos_Espero_RegistrarCredencialComHashESalt()
            {
                // Arrange
                var senhaHash = new byte[] { 10, 20, 30 };
                var senhaSalt = new byte[] { 40, 50, 60 };
                usuarioCredencialRepositorioMock
                    .RecuperarAsync(Arg.Any<Expression<Func<UsuarioCredencial, bool>>>(), cancellationToken)
                    .Returns((UsuarioCredencial?)null);

                autenticacaoServico
                    .When(x => x.CriarSenhaHash(Arg.Any<string>(), out Arg.Any<byte[]>(), out Arg.Any<byte[]>()))
                    .Do(callInfo =>
                    {
                        callInfo[1] = senhaHash;
                        callInfo[2] = senhaSalt;
                    });

                usuarioCredencialRepositorioMock
                    .RegistrarAsync(Arg.Any<UsuarioCredencialComando>(), cancellationToken)
                    .Returns(callInfo => new UsuarioCredencial(callInfo.Arg<UsuarioCredencialComando>()));

                // Act
                var resultado = await sut.RegistrarAsync(usuarioCredencialComando, cancellationToken);

                // Assert
                resultado.UsuarioId.Should().Be(usuarioCredencialComando.UsuarioId);
                resultado.NomeUsuario.Should().Be(usuarioCredencialComando.NomeUsuario);
                resultado.Mensagem.Should().Be("Usuário registrado com sucesso!");
                usuarioCredencialComando.SenhaHash.Should().Equal(senhaHash);
                usuarioCredencialComando.SenhaSalt.Should().Equal(senhaSalt);
                autenticacaoServico.Received(1).CriarSenhaHash(usuarioCredencialComando.Senha,
                    out Arg.Any<byte[]>(), out Arg.Any<byte[]>());
                await usuarioCredencialRepositorioMock.Received(1)
                    .RegistrarAsync(usuarioCredencialComando, cancellationToken);
            }

            [Fact]
            public async Task Quando_IdDeUsuarioJaEstiverVinculado_Espero_LancarExcecaoSemRegistrar()
            {
                // Arrange
                var credencialExistente = CriarCredencial(usuarioCredencialComando.UsuarioId, "outro.usuario");
                ConfigurarCredencialExistente(credencialExistente);

                // Act
                var excecao = await Assert.ThrowsAsync<Exception>(() =>
                    sut.RegistrarAsync(usuarioCredencialComando, cancellationToken));

                // Assert
                excecao.Message.Should().Be("O nome de usuário informado já está em uso!");
                autenticacaoServico.DidNotReceive()
                    .CriarSenhaHash(Arg.Any<string>(), out Arg.Any<byte[]>(), out Arg.Any<byte[]>());
                await usuarioCredencialRepositorioMock.DidNotReceive()
                    .RegistrarAsync(Arg.Any<UsuarioCredencialComando>(), Arg.Any<CancellationToken>());
            }

            [Fact]
            public async Task Quando_NomeDeUsuarioJaEstiverEmUso_Espero_LancarExcecaoSemRegistrar()
            {
                // Arrange
                var credencialExistente = CriarCredencial(2, usuarioCredencialComando.NomeUsuario);
                ConfigurarCredencialExistente(credencialExistente);

                // Act
                var excecao = await Assert.ThrowsAsync<Exception>(() =>
                    sut.RegistrarAsync(usuarioCredencialComando, cancellationToken));

                // Assert
                excecao.Message.Should().Be("O nome de usuário informado já está em uso!");
                autenticacaoServico.DidNotReceive()
                    .CriarSenhaHash(Arg.Any<string>(), out Arg.Any<byte[]>(), out Arg.Any<byte[]>());
                await usuarioCredencialRepositorioMock.DidNotReceive()
                    .RegistrarAsync(Arg.Any<UsuarioCredencialComando>(), Arg.Any<CancellationToken>());
            }

            private void ConfigurarCredencialExistente(UsuarioCredencial credencialExistente)
            {
                usuarioCredencialRepositorioMock
                    .RecuperarAsync(
                        Arg.Is<Expression<Func<UsuarioCredencial, bool>>>(filtro => filtro.Compile()(credencialExistente)),
                        cancellationToken)
                    .Returns(credencialExistente);
            }
        }

        public class LoginAsyncMetodo : UsuariosCredenciaisServicoTestes
        {
            [Fact]
            public async Task Quando_NomeDeUsuarioNaoExistir_Espero_LancarExcecaoDeValidacao()
            {
                // Arrange
                var comando = new UsuarioCredencialLoginComando
                {
                    NomeUsuario = usuarioCredencialComando.NomeUsuario,
                    Senha = usuarioCredencialComando.Senha
                };
                usuarioCredencialRepositorioMock
                    .RecuperarPorNomeUsuarioAsync(comando.NomeUsuario, cancellationToken)
                    .Returns((UsuarioCredencial?)null);

                // Act
                var excecao = await Assert.ThrowsAsync<ArgumentException>(() =>
                    sut.LoginAsync(comando, cancellationToken));

                // Assert
                excecao.Message.Should().Be("O nome de usuário informado não foi encontrado!");
                autenticacaoServico.DidNotReceive()
                    .VerificarLogin(Arg.Any<string>(), Arg.Any<byte[]>(), Arg.Any<byte[]>());
                autenticacaoServico.DidNotReceive()
                    .CriarTokenJwt(Arg.Any<UsuarioCredencial>(), Arg.Any<CancellationToken>());
            }

            [Fact]
            public async Task Quando_SenhaEstiverIncorreta_Espero_LancarExcecaoDeValidacao()
            {
                // Arrange
                var comando = new UsuarioCredencialLoginComando
                {
                    NomeUsuario = usuarioCredencialComando.NomeUsuario,
                    Senha = "senha-incorreta"
                };
                var credencial = CriarCredencial(usuarioCredencialComando.UsuarioId, usuarioCredencialComando.NomeUsuario);
                usuarioCredencialRepositorioMock
                    .RecuperarPorNomeUsuarioAsync(comando.NomeUsuario, cancellationToken)
                    .Returns(credencial);
                autenticacaoServico
                    .VerificarLogin(comando.Senha, credencial.SenhaHash, credencial.SenhaSalt)
                    .Returns(false);

                // Act
                var excecao = await Assert.ThrowsAsync<ArgumentException>(() =>
                    sut.LoginAsync(comando, cancellationToken));

                // Assert
                excecao.Message.Should().Be("A senha informada está incorreta!");
                autenticacaoServico.Received(1)
                    .VerificarLogin(comando.Senha, credencial.SenhaHash, credencial.SenhaSalt);
                autenticacaoServico.DidNotReceive()
                    .CriarTokenJwt(Arg.Any<UsuarioCredencial>(), Arg.Any<CancellationToken>());
            }

            [Fact]
            public async Task Quando_CredenciaisForemValidas_Espero_RetornarDadosDeLoginComToken()
            {
                // Arrange
                var comando = new UsuarioCredencialLoginComando
                {
                    NomeUsuario = usuarioCredencialComando.NomeUsuario,
                    Senha = usuarioCredencialComando.Senha
                };
                var credencial = CriarCredencial(usuarioCredencialComando.UsuarioId, usuarioCredencialComando.NomeUsuario);
                usuarioCredencialRepositorioMock
                    .RecuperarPorNomeUsuarioAsync(comando.NomeUsuario, cancellationToken)
                    .Returns(credencial);
                autenticacaoServico
                    .VerificarLogin(comando.Senha, credencial.SenhaHash, credencial.SenhaSalt)
                    .Returns(true);
                autenticacaoServico
                    .CriarTokenJwt(credencial, cancellationToken)
                    .Returns("token-gerado");

                // Act
                var resultado = await sut.LoginAsync(comando, cancellationToken);

                // Assert
                resultado.NomeUsuario.Should().Be(credencial.NomeUsuario);
                resultado.Token.Should().Be("token-gerado");
                resultado.DataAcesso.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
                autenticacaoServico.Received(1)
                    .VerificarLogin(comando.Senha, credencial.SenhaHash, credencial.SenhaSalt);
                autenticacaoServico.Received(1).CriarTokenJwt(credencial, cancellationToken);
            }
        }
    }
}
