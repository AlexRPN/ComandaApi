using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Produtos.Request
{
    public class ProdutoFiltroRequest
    {
        public int? Id { get; set; }
        public int? EmpresaId { get; set; }
        public int? CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public AtivoInativoEnum? Status { get; set; }
    }
}
