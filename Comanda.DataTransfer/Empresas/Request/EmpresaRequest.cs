using Comanda.DataTransfer.EnderecosEmpresas.Request;
using Comanda.DataTransfer.HorariosFuncionamento.Request;

namespace Comanda.DataTransfer.Empresas.Request
{
    public class EmpresaRequest
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string? Logo { get; set; }
        public string? BannerPrincipal { get; set; }
        public EnderecoEmpresaRequest Endereco { get; set; }
        public IEnumerable<HorarioFuncionamentoRequest> HorariosFuncionamento { get; set; }
    }
}
