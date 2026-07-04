using Comanda.DataTransfer.EnderecoClientes.Request;

namespace Comanda.DataTransfer.Clientes.Request
{
    public class ClienteEditarRequest
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public EnderecoClienteEditarRequest Endereco { get; set; }
    }
}
