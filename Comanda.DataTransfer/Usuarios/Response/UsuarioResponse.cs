using Comanda.DataTransfer.Empresas.Response;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Usuarios.Response
{
    public class UsuarioResponse
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public PerfilEnum Perfil { get; private set; }
        public StatusEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime UltimoAcesso { get; private set; }
        public EmpresaResponse Empresa { get; set; }
    }
}
