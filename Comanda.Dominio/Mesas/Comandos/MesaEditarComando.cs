
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Mesas.Comandos
{
    public class MesaEditarComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int Capacidade { get; set; }
        public SituacaoMesaEnum Situacao { get; set; }
    }
}
