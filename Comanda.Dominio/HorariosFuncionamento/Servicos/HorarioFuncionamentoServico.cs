using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
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
                    Status = StatusEnum.Ativo
                });
            }

            await horarioFuncionamentoRepositorio.InserirAsync(horariosFuncionamento, cancellationToken);
            return horariosFuncionamento;
        }

        public async Task<IEnumerable<HorarioFuncionamento>> EditarAsync(int empresaId, IEnumerable<HorarioFuncionamentoEditarComando> comando, CancellationToken cancellationToken)
        {
            if (!comando.Any())
            {
                throw new ArgumentException("Nenhum horário informado.");
            }

            var horariosExistentes = (await horarioFuncionamentoRepositorio
                .ListarAsync(x => x.EmpresaId == empresaId, cancellationToken))
                .ToList();

            foreach (var item in comando)
            {
                var horarioExistente = horariosExistentes
                    .FirstOrDefault(x => x.DiaSemana == item.DiaSemana);

                if (horarioExistente == null)
                {
                    throw new ArgumentException(
                        $"Horário para o dia {item.DiaSemana} não encontrado.");
                }

                horarioExistente.SetHoraAbertura(item.HoraAbertura);
                horarioExistente.SetHoraFechamento(item.HoraFechamento);

                await horarioFuncionamentoRepositorio.EditarAsync(horarioExistente, cancellationToken);
            }

            return horariosExistentes;
        }

        public async Task<IEnumerable<HorarioFuncionamento>> ValidarAsync(int id, CancellationToken cancellationToken)
        {
            HorarioFuncionamento horarioFuncionamento = await horarioFuncionamentoRepositorio.RecuperarAsync(id, cancellationToken);

            return new List<HorarioFuncionamento> { horarioFuncionamento };
        }
    }
}
