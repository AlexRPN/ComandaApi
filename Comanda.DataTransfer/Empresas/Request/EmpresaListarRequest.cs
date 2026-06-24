
using Comanda.Dominio.Utils.Filtros;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.DataTransfer.Empresas.Request
{
    public class EmpresaListarRequest : PaginacaoFiltro
    {
        public int? Id { get; set; }
        public string? Cnpj { get; set; }

        public EmpresaListarRequest() : base(cpOrd: "Cnpj", tpOrd: TipoOrdenacaoEnum.Asc)
        {
        }
    }
}
