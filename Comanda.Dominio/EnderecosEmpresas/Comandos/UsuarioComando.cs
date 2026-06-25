using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.EnderecosEmpresas.Comandos
{
    public class UsuarioComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public PerfilEnum Perfil { get; set; }
        public AtivoInativoEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimoAcesso { get; set; }
    }
}
