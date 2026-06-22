using Comanda.Dominio.EnderecosEmpresas.Comandos;

namespace Comanda.Dominio.EnderecosEmpresas.Servicos.Interfaces
{
    public interface IEnderecoEmpresaServico
    {
        Task<EnderecoEmpresaComando> InserirAsync(EnderecoEmpresaInserirComando comando, CancellationToken cancellationToken);
    }
}
