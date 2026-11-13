using System.ComponentModel;

namespace Comanda.Dominio.Utils.Enumeradores
{
    public enum SituacaoMesaEnum
    {
        [Description("Livre")]
        Livre = 0,
        [Description("Ocupada")]
        Ocupada = 1,
        [Description("Reservada")]
        Reservada = 2
    }
}
