using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Clientes.Repositorios.Filtros
{
    public class ClienteListarFiltro
    {
        public int? Id { get; set; }
        public int? EmpresaId { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public StatusEnum? Status { get; set; }
    }
}
