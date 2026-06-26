
using Comanda.Dominio.Utils.Enumeradores;
using System.ComponentModel.DataAnnotations;

namespace Comanda.DataTransfer.Usuarios.Request
{
    public class UsuarioRequest
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }
        public PerfilEnum Perfil { get; set; }
    }
}
