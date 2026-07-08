
namespace Comanda.Dominio.Categorias.Comandos
{
    public class CategoriaEditarComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
