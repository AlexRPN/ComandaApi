using Comanda.Dominio.EnderecosEmpresas.Comandos;

namespace Comanda.Dominio.EnderecosEmpresas.Repositorios.Interfaces
{
    public interface IEnderecoEmpresaRepositorio
    {
        Task<EnderecoEmpresaComando> InserirAsync(EnderecoEmpresaComando comando, CancellationToken cancellationToken);
    }
}
