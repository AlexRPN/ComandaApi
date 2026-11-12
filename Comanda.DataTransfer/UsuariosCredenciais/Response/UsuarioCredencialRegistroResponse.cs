
using Comanda.DataTransfer.Usuarios.Response;

namespace Comanda.DataTransfer.UsuariosCredenciais.Response
{
    public class UsuarioCredencialRegistroResponse
    {
        public string NomeUsuario { get; set; }
        public UsuarioResponse Usuario { get; set; }
        public string Mensagem { get; set; }
    }
}
