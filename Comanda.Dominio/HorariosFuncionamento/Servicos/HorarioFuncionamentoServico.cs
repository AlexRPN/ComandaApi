using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
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
        private readonly IEmpresaServico empresaServico;
        public HorarioFuncionamentoServico(IHorarioFuncionamentoRepositorio horarioFuncionamentoRepositorio, 
                                           IEmpresaServico empresaServico)
        {
            this.horarioFuncionamentoRepositorio = horarioFuncionamentoRepositorio;
            this.empresaServico = empresaServico;
        }

        public async Task<IEnumerable<HorarioFuncionamento>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken)
        {
            var horariosFuncionamentosComando = new List<HorarioFuncionamentoComando>();

            var empresaId = comando.First().EmpresaId;

            var empresaValida = await empresaServico.RecuperarAsync(empresaId, cancellationToken);

            if (empresaValida is null)
            {
                throw new ArgumentException("Empresa não encontrada!");
            }

            if(comando.Any(x => x.EmpresaId != empresaId))
            {
                throw new ArgumentException("Todos os horários devem pertencer à mesma empresa!");
            }

            var diasDuplicados = comando.GroupBy(x => x.DiaSemana)
                                        .Any(x => x.Count() > 1);

            if (diasDuplicados)
            {
                throw new ArgumentException("Não é permitido inserir horários de funcionamento duplicados para o mesmo dia da semana!");
            }

            foreach (var item in comando)
            {
                var horarioExistente = await horarioFuncionamentoRepositorio.RecuperarAsync(x => x.EmpresaId == item.EmpresaId &&
                                                                                            x.DiaSemana == item.DiaSemana,
                                                                                            cancellationToken);

                if (horarioExistente is not null)
                {
                    throw new ArgumentException(
                        $"Já existe horário cadastrado para {item.DiaSemana}.");
                }

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

        public async Task<IEnumerable<HorarioFuncionamento>> EditarAsync(IEnumerable<HorarioFuncionamentoEditarComando> comando,
                                                                         CancellationToken cancellationToken)
        {
            var listaComando = comando.ToList();

            if (listaComando.Count == 0)
            {
                throw new ArgumentException("Nenhum horário informado.");
            }

            var codigoEmpresa = listaComando.First().EmpresaId;

            if (listaComando.Any(x => x.EmpresaId != codigoEmpresa))
            {
                throw new ArgumentException("Todos os horários devem pertencer à mesma empresa!");
            }

            var horariosExistentes = (await RecuperarAsync(codigoEmpresa, cancellationToken)).ToList();

            foreach (var item in listaComando)
            {
                var horarioExistente = horariosExistentes.FirstOrDefault(x => x.DiaSemana == item.DiaSemana);

                if (horarioExistente is null)
                {
                    IEnumerable<HorarioFuncionamentoComando> novoHorario =new List<HorarioFuncionamentoComando>
                    {
                        new HorarioFuncionamentoComando
                        {
                            EmpresaId = item.EmpresaId,
                            DiaSemana = item.DiaSemana,
                            HoraAbertura = item.HoraAbertura,
                            HoraFechamento = item.HoraFechamento,
                            Status = StatusEnum.Ativo
                        }
                    };

                    var horariosInseridos = await InserirAsync(novoHorario, cancellationToken);

                    horariosExistentes.AddRange(horariosInseridos);
                }
                else
                {
                    horarioExistente.SetHoraAbertura(item.HoraAbertura);
                    horarioExistente.SetHoraFechamento(item.HoraFechamento);

                    await horarioFuncionamentoRepositorio.EditarAsync(horarioExistente, cancellationToken);
                }
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
