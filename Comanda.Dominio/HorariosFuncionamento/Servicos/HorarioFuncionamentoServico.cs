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

        public async Task<IEnumerable<HorarioFuncionamento>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken)
        {
            var horariosFuncionamentosComando = new List<HorarioFuncionamentoComando>();

            foreach (var item in comando)
            {
                horariosFuncionamentosComando.Add(new HorarioFuncionamentoComando
                {
                    EmpresaId = item.EmpresaId,
                    DiaSemana = item.DiaSemana,
                    HoraAbertura = item.HoraAbertura,
                    HoraFechamento = item.HoraFechamento,
                    Status = StatusEnum.Ativo 
                });
            }

            var horarioFuncionamento = await horarioFuncionamentoRepositorio.InserirAsync(horariosFuncionamentosComando, 
                                                                                          cancellationToken);

            return horarioFuncionamento;
        }

        public async Task<IEnumerable<HorarioFuncionamento>> EditarAsync(int empresaId,             
                                                                        IEnumerable<HorarioFuncionamentoEditarComando> comando, 
                                                                        CancellationToken cancellationToken)
        {
            if (!comando.Any())
            {
                throw new ArgumentException("Nenhum horário informado.");
            }

            var horariosExistentes = await RecuperarAsync(empresaId, cancellationToken);

            foreach (var item in comando)
            {
                var horarioExistente = horariosExistentes.FirstOrDefault(x => x.DiaSemana == item.DiaSemana);

                horarioExistente.SetHoraAbertura(item.HoraAbertura);
                horarioExistente.SetHoraFechamento(item.HoraFechamento);

                await horarioFuncionamentoRepositorio.EditarAsync(horarioExistente, cancellationToken);
            }

            return horariosExistentes;
        }

        public async Task<IEnumerable<HorarioFuncionamento>> RecuperarAsync(int idEmpresa, CancellationToken cancellationToken)
        {
            var horarios = (await horarioFuncionamentoRepositorio.ListarAsync(x => x.EmpresaId == idEmpresa,
                                                                              cancellationToken));

            return horarios;
        }
    }
}
