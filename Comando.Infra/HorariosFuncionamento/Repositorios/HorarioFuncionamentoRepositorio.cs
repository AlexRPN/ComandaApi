
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces;
using Comanda.Infra.Data;

namespace Comanda.Infra.HorariosFuncionamento.Repositorios
{
    public class HorarioFuncionamentoRepositorio : IHorarioFuncionamentoRepositorio
    {
        private readonly AppDbContext appDbContext;
        public HorarioFuncionamentoRepositorio(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<HorarioFuncionamentoComando>> InserirAsync(IEnumerable<HorarioFuncionamentoComando> comando, CancellationToken cancellationToken)
        {
            try
            {
                var horariosFuncionamento = comando
                    .Select(x => new HorarioFuncionamento(x))
                    .ToList();

                await appDbContext.HorariosFuncionamento.AddRangeAsync(
                    horariosFuncionamento,
                    cancellationToken);

                await appDbContext.SaveChangesAsync(cancellationToken);

                for (int i = 0; i < comando.Count(); i++)
                {
                    comando.ElementAt(i).Id = horariosFuncionamento[i].Id;
                }

                return comando;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir horário de funcionamento!", ex);
            }
        }
    }
}
