using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.Genericos;

namespace Comanda.Dominio.EnderecosEmpresas.Repositorios.Interfaces
{
    public interface IEnderecoEmpresaRepositorio : IGenericoRepositorio<EnderecoEmpresa>
    {
        Task<EnderecoEmpresaComando> InserirAsync(EnderecoEmpresaComando comando, CancellationToken cancellationToken);
    }
}
