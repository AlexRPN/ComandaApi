
using Comanda.DataTransfer.Usuarios.Request;

namespace Comanda.DataTransfer.UsuariosCredenciais.Request
{
    public class UsuarioCredencialRegistroRequest
    {
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public UsuarioRequest Usuario { get; set; }
    }
}
