
using System.ComponentModel;

namespace Comanda.Dominio.Utils.Enumeradores
{
    public enum PerfilEnum
    {
        [Description("Administrador")]
        Administrador,

        [Description("Usuário")]
        Usuario,

        [Description("Cliente")]
        Cliente,

        [Description("Entregador")]
        Entregador,

        [Description("Gerente")]
        Gerente
    }
}
