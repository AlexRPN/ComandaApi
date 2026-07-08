
namespace Comanda.DataTransfer.Categorias.Request
{
    public class CategoriaEditarRequest
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
