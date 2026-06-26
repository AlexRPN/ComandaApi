using Comanda.Dominio.Utils.Autenticacoes.Servicos.Interfaces;
using System.Security.Cryptography;

namespace Comanda.Dominio.Utils.Autenticacoes.Servicos
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificarLogin(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                var hashUsuario = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return hashUsuario.SequenceEqual(senhaHash);
            }
        }
    }
}
