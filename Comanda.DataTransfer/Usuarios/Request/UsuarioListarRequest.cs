using Comanda.Dominio.Utils.Filtros;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.DataTransfer.Usuarios.Request
{
    public class UsuarioListarRequest : PaginacaoFiltro
    {
        public int? Id { get; set; }
        public string? Cpf { get; set; }
        public UsuarioListarRequest() : base(cpOrd: "Cpf", tpOrd: TipoOrdenacaoEnum.Asc)
        {
        }
    }
}
