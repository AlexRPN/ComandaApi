using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.HorariosFuncionamento.Comando
{
    public class HorarioFuncionamentoInserirComando
    {
        public int EmpresaId { get; set; }
        public DiaSemanaEnum DiaSemana { get; set; }
        public DateTime HoraAbertura { get; set; }
        public DateTime HoraFechamento { get; set; }
        public AtivoInativoEnum Status { get; set; } = AtivoInativoEnum.Ativo;
    }
}
