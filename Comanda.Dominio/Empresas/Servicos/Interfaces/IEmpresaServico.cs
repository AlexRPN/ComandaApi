
using Comanda.Dominio.Empresas.Comandos;

namespace Comanda.Dominio.Empresas.Servicos.Interfaces
{
    public interface IEmpresaServico
    {
        Task<EmpresaComando> InserirAsync(EmpresaInserirComando comando, CancellationToken cancellationToken);
    }
}
