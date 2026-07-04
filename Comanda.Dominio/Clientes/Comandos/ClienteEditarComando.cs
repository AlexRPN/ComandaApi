
namespace Comanda.Dominio.Clientes.Comandos
{
    public class ClienteEditarComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
    }
}
