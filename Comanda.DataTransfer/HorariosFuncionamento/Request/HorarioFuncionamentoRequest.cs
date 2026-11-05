using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.HorariosFuncionamento.Request
{
    public class HorarioFuncionamentoRequest
    {
        public int EmpresaId { get; set; }
        public DiaSemanaEnum DiaSemana { get; set; }
        public DateTime HoraAbertura { get; set; }
        public DateTime HoraFechamento { get; set; }
    }
}
