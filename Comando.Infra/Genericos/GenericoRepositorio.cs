using System.Linq.Dynamic.Core;
using Comanda.Dominio.Genericos;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using Comanda.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Genericos
{
    public class GenericoRepositorio<T> : IGenericoRepositorio<T> where T : class
    {
        protected readonly AppDbContext appDbContext;
        public GenericoRepositorio(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public T Editar(T entidade)
        {
            appDbContext.Update(entidade);
            return entidade;
        }

        public void Excluir(T entidade)
        {
            appDbContext.Remove(entidade);
        }

        public T Inserir(T entidade)
        {
            appDbContext.Add(entidade);
            return entidade;
        }

        public async Task InserirAsync(IEnumerable<T> entidades)
        {
            foreach (T entidade in entidades)
            {
                await appDbContext.AddAsync(entidade);
            }
        }

        public PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd)
        {
            try
            {
                query = query.OrderBy(cpOrd + " " + tpOrd.ToString());
                return Paginar(query, qt, pg);
            }
            catch
            {
                throw new ArgumentException("Campo da ordenação não informado");
            }
        }

        private static PaginacaoConsulta<T> Paginar(IQueryable<T> query, int qt, int pg)
        {
            return new PaginacaoConsulta<T>
            {
                Registros = query.Skip((pg - 1) * qt).Take(qt).ToList(),
                Total = query.LongCount(),
            };
        }

        public IQueryable<T> Query()
        {
            return appDbContext.Set<T>().AsQueryable();
        }

        public IList<T> QueryList()
        {
            return appDbContext.Set<T>().ToList();
        }


        public T Recuperar(int id)
        {
            return appDbContext.Set<T>().Find(id);
        }

        public async Task<T> InserirAsync(T entidade)
        {
            await appDbContext.AddAsync(entidade);
            return entidade;
        }

        public async Task<IList<T>> ListarAsync()
        {
            var query = await appDbContext.Set<T>().ToListAsync();
            return query;
        }

        public async Task<T> RecuperarAsync(int id)
        {
            var retorno = await appDbContext.Set<T>().FindAsync(id);
            return retorno;
        }

        public async Task<T> EditarAsync(T entidade)
        {
            appDbContext.Update(entidade);
            return entidade;
        }

        public async Task<PaginacaoConsulta<T>> ListarAsync(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken)
        {
            try
            {
                query = query.OrderBy(cpOrd + " " + tpOrd.ToString());
                return await PaginarAsync(query, qt, pg, cancellationToken);
            }
            catch
            {
                throw new ArgumentException("Campo da ordenação não informado");
            }
        }

        private static async Task<PaginacaoConsulta<T>> PaginarAsync(IQueryable<T> query, int qt, int pg, CancellationToken cancellationToken)
        {
            var total = await query.LongCountAsync(cancellationToken);
            var registros = await query.Skip((pg - 1) * qt).Take(qt).ToListAsync(cancellationToken);

            return new PaginacaoConsulta<T>
            {
                Registros = registros,
                Total = total,
            };
        }

        public async Task ExcluirAsync(T entidade)
        {
            appDbContext.Remove(entidade);
        }

        public async Task<IQueryable<T>> QueryAsync()
        {
            List<T> resultado = await appDbContext.Set<T>().ToListAsync();
            return resultado.AsQueryable();
        }

    }
}
