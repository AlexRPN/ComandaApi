using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Usuarios.Comandos
{
    public class UsuarioComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
