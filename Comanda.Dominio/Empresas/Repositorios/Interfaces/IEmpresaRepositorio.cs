using Comanda.Dominio.Empresas.Comandos;

namespace Comanda.Dominio.Empresas.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Task<EmpresaComando> InserirAsync(EmpresaComando comando, CancellationToken cancellationToken);
    }
}
