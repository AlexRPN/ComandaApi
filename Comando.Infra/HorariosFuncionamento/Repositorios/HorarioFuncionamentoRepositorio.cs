
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.HorariosFuncionamento.Repositorios
{
    public class HorarioFuncionamentoRepositorio : GenericoRepositorio<HorarioFuncionamento>, IHorarioFuncionamentoRepositorio
    {
        public HorarioFuncionamentoRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<HorarioFuncionamento>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken)
        {
            try
            {
                var horariosFuncionamento = comando.Select(x => new HorarioFuncionamento(x))
                                                   .ToList();

                await appDbContext.HorariosFuncionamento.AddRangeAsync(horariosFuncionamento, cancellationToken);

                await appDbContext.SaveChangesAsync(cancellationToken);

                return horariosFuncionamento;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir horário de funcionamento!", ex);
            }
        }
    }
}
