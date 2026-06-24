using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;

namespace Comanda.Dominio.EnderecosEmpresas.Servicos.Interfaces
{
    public interface IEnderecoEmpresaServico
    {
        Task<EnderecoEmpresaComando> InserirAsync(EnderecoEmpresaInserirComando comando, CancellationToken cancellationToken);
        Task<EnderecoEmpresa> EditarAsync(EnderecoEmpresaEditarComando comando, CancellationToken cancellationToken);
        Task<EnderecoEmpresa> ValidarAsync(int id, CancellationToken cancellationToken);
    }
}
