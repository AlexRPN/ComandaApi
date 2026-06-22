using Comanda.DataTransfer.EnderecosEmpresas.Response;
using Comanda.DataTransfer.HorariosFuncionamento.Response;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Empresas.Response
{
    public class EmpresaResponse
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string BannerPrincipal { get; set; }
        public AtivoInativoEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public EnderecoEmpresaResponse Endereco { get; set; }
        public IEnumerable<HorarioFuncionamentoResponse> HorariosFuncionamento { get; set; }
        public string Mensagem { get; set; }
    }
}
