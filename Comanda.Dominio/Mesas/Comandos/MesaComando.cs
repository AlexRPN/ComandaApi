
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Mesas.Comandos
{
    public class MesaComando
    {
        public int EmpresaId { get; set; }
        public int Numero { get; set; }
        public int Capacidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public SituacaoMesaEnum Situacao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
