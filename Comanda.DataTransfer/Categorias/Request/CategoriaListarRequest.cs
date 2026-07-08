using Comanda.Dominio.Utils.Filtros;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.DataTransfer.Categorias.Request
{
    public class CategoriaListarRequest : PaginacaoFiltro
    {
        public int? Id { get; set; }
        public int? EmpresaId { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataCadastro { get; set; }
        public CategoriaListarRequest() : base(cpOrd: "Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
        }
    }
}
