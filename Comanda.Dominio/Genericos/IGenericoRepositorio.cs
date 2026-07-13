
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using System.Linq.Expressions;

namespace Comanda.Dominio.Genericos
{
    public interface IGenericoRepositorio<T> where T : class
    {
        T Recuperar(int codigo);

        T Inserir(T entidade);

        T Editar(T entidade);

        void Excluir(T entidade);
        Task InserirAsync(IEnumerable<T> entidades);

        PaginacaoConsulta<T> Listar(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd);

        IQueryable<T> Query();

        IList<T> QueryList();

        Task<IQueryable<T>> QueryAsync();
        Task<T?> RecuperarAsync(int id, CancellationToken cancellationToken);
        Task<T?> RecuperarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<T> InserirAsync(T entidade);

        Task<T> EditarAsync(T entidade, CancellationToken cancellationToken);
        Task ExcluirAsync(T entidade);
        Task<PaginacaoConsulta<T>> ListarPaginadoAsync(IQueryable<T> query, int qt, int pg, string cpOrd, TipoOrdenacaoEnum tpOrd, CancellationToken cancellationToken);
        Task<IEnumerable<T>> ListarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<bool> ValidarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
