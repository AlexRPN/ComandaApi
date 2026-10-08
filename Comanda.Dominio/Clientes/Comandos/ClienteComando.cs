using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Clientes.Comandos
{
    public class ClienteComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int PontosFidelidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
