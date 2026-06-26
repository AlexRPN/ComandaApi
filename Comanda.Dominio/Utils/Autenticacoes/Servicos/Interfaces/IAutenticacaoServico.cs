namespace Comanda.Dominio.Utils.Autenticacoes.Servicos.Interfaces
{
    public interface IAutenticacaoServico
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificarLogin(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}
