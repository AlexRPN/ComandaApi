using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Utils.Status.Request
{
    public class AlterarStatusRequest
    {
        public int Id { get; set; }
        public AtivoInativoEnum Status { get; set; }
    }
}
