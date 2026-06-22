using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.HorariosFuncionamento.Request
{
    public class HorarioFuncionamentoRequest
    {
        public DiaSemanaEnum DiaSemana { get; set; }
        public DateTime HoraAbertura { get; set; }
        public DateTime HoraFechamento { get; set; }
    }
}
