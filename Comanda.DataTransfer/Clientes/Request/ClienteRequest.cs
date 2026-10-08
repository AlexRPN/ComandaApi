using Comanda.DataTransfer.EnderecoClientes.Request;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Clientes.Request
{
    public class ClienteRequest
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int? PontosFidelidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusEnum Status { get; set; }
        public EnderecoClienteRequest Endereco { get; set; }
    }
}
