using Comanda.Dominio.Genericos;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Filtros;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Usuarios.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IGenericoRepositorio<Usuario>
    {
        Task<Usuario> InserirAsync(UsuarioComando comando, CancellationToken cancellationToken);
        Task<Usuario> ValidarCpfAsync(string cpf, CancellationToken cancellationToken);
        Task<Usuario> RecuperarPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IQueryable<Usuario>> FiltrarAsync(UsuarioListarFiltro comando, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<Usuario>> ListarPaginadoAsync(IQueryable<Usuario> query, int qt, int pg, string cpOrd,
                                                             TipoOrdenacaoEnum tpOrd,
                                                             CancellationToken cancellationToken);
    }
}
