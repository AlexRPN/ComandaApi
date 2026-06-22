using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Infra.Data;

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
    }
}
