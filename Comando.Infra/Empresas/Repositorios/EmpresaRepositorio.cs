using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Comanda.Dominio.Empresas.Repositorios.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Empresas.Repositorios
{
    public class EmpresaRepositorio : GenericoRepositorio<Empresa>, IEmpresaRepositorio
    {
        public EmpresaRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
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

        public async Task<IQueryable<Empresa>> FiltrarAsync(EmpresaListarFiltro comando, CancellationToken cancellationToken)
        {
            IQueryable<Empresa> query = appDbContext.Empresas
                .AsNoTracking()
                .Include(e => e.EnderecoEmpresa)
                .Include(e => e.HorariosFuncionamento)
                .AsQueryable();

            if (comando.Id.HasValue)
            {
                query = query.Where(e => e.Id == comando.Id);
            }

            if (!string.IsNullOrWhiteSpace(comando.Cnpj))
            {
                query = query.Where(e => e.Cnpj == comando.Cnpj);
            }

            return query;
        }

        public async Task<Empresa> RecuperarAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await appDbContext.Empresas
                    .AsNoTracking()
                    .Include(e => e.EnderecoEmpresa)
                    .Include(e => e.HorariosFuncionamento)
                    .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return empresa;
        }

        public async Task<PaginacaoConsulta<Empresa>> ListarAsync(IQueryable<Empresa> query, int qt, int pg, string cpOrd,
                                                            TipoOrdenacaoEnum tpOrd,
                                                            CancellationToken cancellationToken)
        {
            return await base.ListarPaginadoAsync(query, qt, pg, cpOrd, tpOrd, cancellationToken);
        }
    }
}
