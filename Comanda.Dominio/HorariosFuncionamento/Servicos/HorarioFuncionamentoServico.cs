using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Servicos.Interfaces;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.HorariosFuncionamento.Servicos
{
    public class HorarioFuncionamentoServico : IHorarioFuncionamentoServico
    {
        private readonly IHorarioFuncionamentoRepositorio horarioFuncionamentoRepositorio;
        public HorarioFuncionamentoServico(IHorarioFuncionamentoRepositorio horarioFuncionamentoRepositorio)
        {
            this.horarioFuncionamentoRepositorio = horarioFuncionamentoRepositorio;
        }

        public async Task<IEnumerable<HorarioFuncionamentoComando>> InserirAsync(IEnumerable<HorarioFuncionamentoInserirComando> comando, CancellationToken cancellationToken)
        {
            var horariosFuncionamento = new List<HorarioFuncionamentoComando>();
            foreach (var item in comando)
            {
                horariosFuncionamento.Add(new HorarioFuncionamentoComando
                {
                    EmpresaId = item.EmpresaId,
                    DiaSemana = item.DiaSemana,
                    HoraAbertura = item.HoraAbertura,
                    HoraFechamento = item.HoraFechamento,
                    Status = AtivoInativoEnum.Ativo
                });
            }
            
            await horarioFuncionamentoRepositorio.InserirAsync(horariosFuncionamento, cancellationToken);
            return horariosFuncionamento;
        }
    }
}
