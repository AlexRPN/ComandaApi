
namespace Comanda.Dominio.EnderecoClientes.Comandos
{
    public class EnderecoClienteComando
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string PontoReferencia { get; set; }
    }
}
