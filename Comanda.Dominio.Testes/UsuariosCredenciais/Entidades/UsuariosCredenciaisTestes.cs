
using Comanda.Dominio.UsuariosCredenciais.Comandos;
using Comanda.Dominio.UsuariosCredenciais.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.UsuariosCredenciais.Entidades
{
    public class UsuariosCredenciaisTestes
    {
        private readonly UsuarioCredencial sut;
        public UsuariosCredenciaisTestes()
        {
            sut = Builder<UsuarioCredencial>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new UsuarioCredencialComando
                {
                    UsuarioId = 1,
                    NomeUsuario = "usuario_teste",
                    SenhaHash = new byte[] { 1, 2, 3 },
                    SenhaSalt = new byte[] { 4, 5, 6 },
                    ConfirmarSenha = "senha_teste",
                    DataAlteracaoSenha = DateTime.Now.Date,
                    UltimoAcesso = DateTime.Now.Date,
                    DataFimBloqueio = DateTime.Now.Date.AddDays(1)
                };

                var usuarioCredencial = new UsuarioCredencial(comando);

                Assert.Equal(comando.UsuarioId, usuarioCredencial.UsuarioId);
                Assert.Equal(comando.NomeUsuario, usuarioCredencial.NomeUsuario);
                Assert.Equal(comando.SenhaHash, usuarioCredencial.SenhaHash);
                Assert.Equal(comando.SenhaSalt, usuarioCredencial.SenhaSalt);
                Assert.Equal(comando.DataAlteracaoSenha, usuarioCredencial.DataAlteracaoSenha);
                Assert.Equal(comando.UltimoAcesso, usuarioCredencial.UltimoAcesso);
                Assert.Equal(comando.DataFimBloqueio, usuarioCredencial.DataFimBloqueio);
            }
        }

        public class SetUsuarioId : UsuariosCredenciaisTestes
        {
            [Fact]
            public void Quando_UsuarioIdForValido_Espero_PropriedadeAtualizada()
            {
                var usuarioId = 2;

                sut.SetUsuarioId(usuarioId);

                Assert.Equal(usuarioId, sut.UsuarioId);
            }
        }

        public class SetNomeUsuario : UsuariosCredenciaisTestes
        {
            [Fact]
            public void Quando_NomeUsuarioForValido_Espero_PropriedadeAtualizada()
            {
                var nomeUsuario = "novo_usuario";

                sut.SetNomeUsuario(nomeUsuario);

                Assert.Equal(nomeUsuario, sut.NomeUsuario);
            }

            [Fact]
            public void Quando_NomeUsuarioForNulo_Espero_PropriedadeAtualizada()
            {
                string nomeUsuario = null;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetNomeUsuario(nomeUsuario));
                Assert.Equal("Nome de usuário deve ser informado!", exception.Message);
            }

            [Fact]
            public void Quando_NomeUsuarioForVazio_Espero_PropriedadeAtualizada()
            {
                string nomeUsuario = string.Empty;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetNomeUsuario(nomeUsuario));
                Assert.Equal("Nome de usuário deve ser informado!", exception.Message);
            }
        }

        public class SetSenhaHash : UsuariosCredenciaisTestes
        {
            [Fact]
            public void Quando_SenhaHashForValido_Espero_PropriedadeAtualizada()
            {
                var senhaHash = new byte[] { 7, 8, 9 };

                sut.SetSenhaHash(senhaHash);

                Assert.Equal(senhaHash, sut.SenhaHash);
            }

            [Fact]
            public void Quando_SenhaHashForNulo_Espero_Excecao()
            {
                byte[] senhaHash = null;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetSenhaHash(senhaHash));

                Assert.Equal("SenhaHash não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_SenhaHashForVazio_Espero_Excecao()
            {
                byte[] senhaHash = new byte[0];

                var exception = Assert.Throws<ArgumentException>(() => sut.SetSenhaHash(senhaHash));

                Assert.Equal("SenhaHash não pode ser nulo ou vazio.", exception.Message);
            }
        }

        public class SetSenhaSalt : UsuariosCredenciaisTestes
        {
            [Fact]
            public void Quando_SenhaSaltForValido_Espero_PropriedadeAtualizada()
            {
                var senhaSalt = new byte[] { 10, 11, 12 };

                sut.SetSenhaSalt(senhaSalt);

                Assert.Equal(senhaSalt, sut.SenhaSalt);
            }

            [Fact]
            public void Quando_SenhaSaltForNulo_Espero_Excecao()
            {
                byte[] senhaSalt = null;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetSenhaSalt(senhaSalt));

                Assert.Equal("SenhaSalt não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_SenhaSaltForVazio_Espero_Excecao()
            {
                byte[] senhaSalt = new byte[0];

                var exception = Assert.Throws<ArgumentException>(() => sut.SetSenhaSalt(senhaSalt));

                Assert.Equal("SenhaSalt não pode ser nulo ou vazio.", exception.Message);
            }
        }

        public class SetDataAlteracaoSenha : UsuariosCredenciaisTestes
        {
            [Fact]
            public void Quando_DataAlteracaoSenhaForValido_Espero_PropriedadeAtualizada()
            {
                var dataAlteracaoSenha = DateTime.Now.Date;

                sut.SetDataAlteracaoSenha(dataAlteracaoSenha);

                Assert.Equal(dataAlteracaoSenha, sut.DataAlteracaoSenha);
            }
        }

        public class SetUltimoAcesso : UsuariosCredenciaisTestes
        {
            [Fact]
            public void Quando_UltimoAcessoForValido_Espero_PropriedadeAtualizada()
            {
                var ultimoAcesso = DateTime.Now.Date;

                sut.SetUltimoAcesso(ultimoAcesso);

                Assert.Equal(ultimoAcesso, sut.UltimoAcesso);
            }
        }

        public class SetDataFimBloqueio : UsuariosCredenciaisTestes
        {
            [Fact]
            public void Quando_DataFimBloqueioForValido_Espero_PropriedadeAtualizada()
            {
                var dataFimBloqueio = DateTime.Now.Date.AddDays(1);

                sut.SetDataFimBloqueio(dataFimBloqueio);

                Assert.Equal(dataFimBloqueio, sut.DataFimBloqueio);
            }

            [Fact]
            public void Quando_DataFimBloqueioForNulo_Espero_PropriedadeAtualizada()
            {
                DateTime? dataFimBloqueio = null;

                sut.SetDataFimBloqueio(dataFimBloqueio);

                Assert.Null(sut.DataFimBloqueio);
            }

            [Fact]
            public void Quando_DataFimBloqueioForAnteriorADataInicioBloqueio_Espero_Excecao()
            {
                var dataInicioBloqueio = DateTime.Now.Date;
                var dataFimBloqueio = dataInicioBloqueio.AddDays(-1);

                var exception = Assert.Throws<ArgumentException>(() => sut.SetDataFimBloqueio(dataFimBloqueio));

                Assert.Equal("Data de fim de bloqueio não pode ser anterior à data atual!", exception.Message);
            }

            [Fact]
            public void Quando_DataFimBloqueioPossuirValorAnteriorADataInicioBloqueio_Espero_Excecao()
            {
                var dataInicioBloqueio = DateTime.Now.Date;
                var dataFimBloqueio = dataInicioBloqueio.AddDays(-1);

                var exception = Assert.Throws<ArgumentException>(() => sut.SetDataFimBloqueio(dataFimBloqueio));

                Assert.Equal("Data de fim de bloqueio não pode ser anterior à data atual!", exception.Message);
            }
        }
    }
}
