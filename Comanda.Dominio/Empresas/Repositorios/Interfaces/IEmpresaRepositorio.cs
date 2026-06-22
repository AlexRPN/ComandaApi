using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;

namespace Comanda.Dominio.Empresas.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Task<EmpresaComando> InserirAsync(EmpresaComando comando, CancellationToken cancellationToken);
        Task<Empresa> RecuperarAsync(int id, CancellationToken cancellationToken);
    }
}
