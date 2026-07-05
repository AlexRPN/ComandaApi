using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Clientes.Repositorios.Filtros;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;

namespace Comanda.Dominio.Clientes.Servicos.Interfaces
{
    public interface IClienteServico
    {
        Task<Cliente> InserirAsync(ClienteComando comando, CancellationToken cancellationToken);
        Task<Cliente> EditarAsync(ClienteEditarComando comando, CancellationToken cancellationToken);
        Task<Cliente> RecuperarAsync(int id, CancellationToken cancellationToken);
        Task<IQueryable<Cliente>> FiltrarAsync(ClienteListarFiltro filtro,  CancellationToken cancellationToken);
        Task<PaginacaoConsulta<Cliente>> ListarPaginadoAsync(IQueryable<Cliente> query, int qt, int pg, string cpOrd,
                                                             TipoOrdenacaoEnum tpOrd,
                                                             CancellationToken cancellationToken);
        Task<Cliente> AlterarStatusAsync(int id, AtivoInativoEnum status, CancellationToken cancellationToken);
    }
}
