using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Usuarios.Request
{
    public class UsuarioRequest
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
    }
}
