using Comanda.DataTransfer.EnderecoClientes.Response;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Clientes.Response
{
    public class ClienteResponse
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int PontosFidelidade { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }
        public StatusEnum? Status { get; set; }
        public EnderecoClienteResponse Endereco { get; set; }
    }
}
