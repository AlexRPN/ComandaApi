using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.DataTransfer.Clientes.Request
{
    public class ClienteListarRequest : PaginacaoFiltro
    {
        public int? Id { get; set; }
        public int? EmpresaId { get; set; }
        public string? Nome { get; set; }
        public string? Telefone { get; set; }
        public AtivoInativoEnum? Status { get; set; }

        public ClienteListarRequest() : base(cpOrd: "Nome", tpOrd: TipoOrdenacaoEnum.Asc)
        {
        }
    }
}
