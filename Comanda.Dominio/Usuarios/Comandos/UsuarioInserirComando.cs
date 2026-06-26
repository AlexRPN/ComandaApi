
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Usuarios.Comandos
{
    public class UsuarioInserirComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public PerfilEnum Perfil { get; set; }
        public AtivoInativoEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimoAcesso { get; set; }
    }
}
