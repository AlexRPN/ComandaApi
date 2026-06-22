
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;

namespace Comanda.Dominio.Empresas.Servicos.Interfaces
{
    public interface IEmpresaServico
    {
        Task<EmpresaComando> InserirAsync(EmpresaInserirComando comando, CancellationToken cancellationToken);
        Task<Empresa> RecuperarAsync(int id, CancellationToken cancellationToken);
    }
}
