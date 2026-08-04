
namespace Comanda.Dominio.Categorias.Repositorios.Filtros
{
    public class CategoriaListarFiltro
    {
        public int? Id { get; set; }
        public int? EmpresaId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
