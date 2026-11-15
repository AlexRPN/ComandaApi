
using System.ComponentModel;

namespace Comanda.Dominio.Utils.Enumeradores
{
    public enum SituacaoPedidoEnum
    {
        [Description("Recebido")]
        Recebido,

        [Description("Confirmado")]
        Confirmado,

        [Description("Preparando")]
        Preparando,

        [Description("Pronto")]
        Pronto,

        [Description("Saiu para entrega")]
        SaiuParaEntrega,

        [Description("Entregue")]
        Entregue,

        [Description("Cancelado")]
        Cancelado
    }
}
