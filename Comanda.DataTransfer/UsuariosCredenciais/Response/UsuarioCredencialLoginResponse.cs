
namespace Comanda.DataTransfer.UsuariosCredenciais.Response
{
    public class UsuarioCredencialLoginResponse
    {
        public string NomeUsuario { get; set; }
        public string Token { get; set; }
        public DateTime DataAcesso { get; set; }
        public string Mensagem { get; set; }
    }
}
