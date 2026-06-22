using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.HorariosFuncionamento.Response
{
    public class HorarioFuncionamentoResponse
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public DiaSemanaEnum DiaSemana { get; set; }
        public DateTime HoraAbertura { get; set; }
        public DateTime HoraFechamento { get; set; }
    }
}
