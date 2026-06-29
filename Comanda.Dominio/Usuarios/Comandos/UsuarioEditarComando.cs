using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Usuarios.Comandos
{
    public class UsuarioEditarComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
    }
}
