
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Empresas.Comandos
{
    public class EmpresaInserirComando
    {
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string BannerPrincipal { get; set; }
        public AtivoInativoEnum Status { get; set; } = AtivoInativoEnum.Ativo;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
