using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.DataTransfer.Produtos.Request
{
    public class ProdutoListarRequest : PaginacaoFiltro
    {
        public ProdutoListarRequest() : base(cpOrd: "Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
        }

        public int? Id { get; set; }
        public int? EmpresaId { get; set; }
        public int? CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public StatusEnum? Status { get; set; }
    }
}
