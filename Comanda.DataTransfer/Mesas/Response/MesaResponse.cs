using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Mesas.Response
{
    public class MesaResponse
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int Numero { get; set; }
        public int Capacidade { get; set; }
        public SituacaoMesaEnum Situacao { get; set; }
        public StatusEnum Status { get; set; }
        public string Mensagem { get; set; }
    }
}
