
using System.ComponentModel;

namespace Comanda.Dominio.Utils.Enumeradores
{
    public enum TipoPedidoEnum
    {
        [Description("Delivery")]
        Delivery,

        [Description("Retirada")]
        Retirada,

        [Description("Mesa")]
        Mesa
    }
}
