using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Mesas.Request
{
    public class MesaEditarRequest
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int Capacidade { get; set; }
        public SituacaoMesaEnum Situacao { get; set; }
    }
}
