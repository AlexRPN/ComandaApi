
using System.ComponentModel.DataAnnotations;

namespace Comanda.DataTransfer.EnderecoClientes.Request
{
    public class EnderecoClienteRequest
    {
        public int ClienteId { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string? Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        [MaxLength(2)]
        public string Estado { get; set; }
        public string? PontoReferencia { get; set; }
    }
}
