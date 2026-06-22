using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Empresas.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly AppDbContext appDbContext;
        public EmpresaRepositorio(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<EmpresaComando> InserirAsync(EmpresaComando comando, CancellationToken cancellationToken)
        {
            try
            {
                var empresa = new Empresa(comando);

                await appDbContext.Empresas.AddAsync(empresa, cancellationToken);

                await appDbContext.SaveChangesAsync(cancellationToken);
                comando.Id = empresa.Id;

                return comando;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir Empresa!", ex);
            }
        }

        public async Task<Empresa> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await appDbContext.Empresas.AsNoTracking()
                    .Include(e => e.EnderecoEmpresa)
                    .Include(e => e.HorariosFuncionamento)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return empresa;
        }
    }
}
