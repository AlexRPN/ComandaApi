
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.HorariosFuncionamento.Entidades
{
    public class HorarioFuncionamento
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        #endregion

        public int Id { get; private set; }
        public DiaSemanaEnum DiaSemana { get; private set; }
        public DateTime HoraAbertura { get; private set; }
        public DateTime HoraFechamento { get; private set; }
        public AtivoInativoEnum Status { get; private set; }

        private HorarioFuncionamento()
        {

        }

        public HorarioFuncionamento(HorarioFuncionamentoComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetDiaSemana(comando.DiaSemana);
            SetHoraAbertura(comando.HoraAbertura);
            SetHoraFechamento(comando.HoraFechamento);
            Status = comando.Status;
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetDiaSemana(DiaSemanaEnum diaSemana)
        {
            DiaSemana = diaSemana;
        }

        public void SetHoraAbertura(DateTime horaAbertura)
        {
            HoraAbertura = horaAbertura;
        }

        public void SetHoraFechamento(DateTime horaFechamento)
        {
            HoraFechamento = horaFechamento;
        }
    }
}
