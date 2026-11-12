
namespace Comanda.Dominio.UsuariosCredenciais.Comandos
{
    public class UsuarioCredencialComando
    {
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public DateTime DataAlteracaoSenha { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public DateTime DataAcesso { get; set; }
        public DateTime? DataFimBloqueio { get; set; }
        public string Token { get; set; }
    }
}
