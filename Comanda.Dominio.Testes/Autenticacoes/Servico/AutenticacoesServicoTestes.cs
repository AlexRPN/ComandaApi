
using Comanda.Dominio.Utils.Autenticacoes.Servicos;

namespace Comanda.Dominio.Testes.Autenticacoes.Servico
{
    public class AutenticacoesServicoTestes
    {
        private readonly AutenticacaoServico sut;

        public AutenticacoesServicoTestes()
        {
            sut = new AutenticacaoServico();
        }

        [Fact]
        public void Quando_CriarSenhaHash_Espero_HashESaltGerados()
        {
            byte[] senhaHash;
            byte[] senhaSalt;

            sut.CriarSenhaHash("senha_teste", out senhaHash, out senhaSalt);

            Assert.Equal(64, senhaHash.Length);
            Assert.NotEmpty(senhaSalt);
        }

        [Fact]
        public void Quando_VerificarLoginComSenhaCorreta_Espero_RetornarVerdadeiro()
        {
            byte[] senhaHash;
            byte[] senhaSalt;

            sut.CriarSenhaHash("senha_teste", out senhaHash, out senhaSalt);

            var resultado = sut.VerificarLogin("senha_teste", senhaHash, senhaSalt);

            Assert.True(resultado);
        }

        [Fact]
        public void Quando_VerificarLoginComSenhaIncorreta_Espero_RetornarFalso()
        {
            byte[] senhaHash;
            byte[] senhaSalt;

            sut.CriarSenhaHash("senha_teste", out senhaHash, out senhaSalt);

            var resultado = sut.VerificarLogin("outra_senha", senhaHash, senhaSalt);

            Assert.False(resultado);
        }
    }
}
