
namespace Comanda.Dominio.UsuariosCredenciais.Comandos
{
    public class UsuarioCredencialComando
    {
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public string ConfirmarSenha { get; set; }
        public DateTime DataAlteracaoSenha { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public DateTime? DataFimBloqueio { get; set; }
    }
}
