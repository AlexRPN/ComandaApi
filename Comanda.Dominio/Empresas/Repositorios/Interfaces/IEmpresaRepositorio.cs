using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Comanda.Dominio.Genericos;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Empresas.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio : IGenericoRepositorio<Empresa>
    {
        Task<EmpresaComando> InserirAsync(EmpresaComando comando, CancellationToken cancellationToken);
        Task<Empresa> RecuperarAsync(int id, CancellationToken cancellationToken);
        Task<IQueryable<Empresa>> FiltrarAsync(EmpresaListarFiltro comando, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<Empresa>> ListarAsync(IQueryable<Empresa> query, int qt, int pg, string cpOrd,
                                                     TipoOrdenacaoEnum tpOrd,
                                                     CancellationToken cancellationToken);
    }
}
