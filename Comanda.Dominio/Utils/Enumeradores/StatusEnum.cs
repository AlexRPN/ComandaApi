using System.ComponentModel;

namespace Comanda.Dominio.Utils.Enumeradores
{
    public enum StatusEnum
    {
        [Description("Inativo")]
        Inativo = 0,

        [Description("Ativo")]
        Ativo = 1,

        [Description("Bloqueado")]
        Bloqueado = 2
    }
}
