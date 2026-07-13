
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Repositorios.Filtros;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Empresas.Servicos.Interfaces
{
    public interface IEmpresaServico
    {
        Task<EmpresaComando> InserirAsync(EmpresaInserirComando comando, CancellationToken cancellationToken);
        Task<Empresa> EditarAsync(EmpresaEditarComando comando, CancellationToken cancellationToken);
        Task<Empresa> ValidarAsync(int id, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<Empresa>> ListarAsync(IQueryable<Empresa> query, int qt, int pg, string cpOrd,
                                                     TipoOrdenacaoEnum tpOrd,
                                                     CancellationToken cancellationToken);
        Task<IQueryable<Empresa>> FiltrarAsync(EmpresaListarFiltro comando, CancellationToken cancellationToken);
    }
}
