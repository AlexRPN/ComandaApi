using Comanda.DataTransfer.Usuarios.Request;
using Comanda.DataTransfer.Usuarios.Response;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Aplicacao.Usuarios.Servicos.Interfaces
{
    public interface IUsuarioAppServico
    {
        Task<string> InserirAsync(UsuarioRequest request, CancellationToken cancellationToken);
        Task<UsuarioResponse> RecuperarPorIdAsync(int id, CancellationToken cancellationToken);
        Task<PaginacaoConsulta<UsuarioResponse>> ListarPaginadoAsync(UsuarioListarRequest request, CancellationToken cancellationToken);
        Task<string> EditarAsync(UsuarioEditarRequest request, CancellationToken cancellationToken);
        Task<string> AlterarStatusAsync(int id, StatusEnum status, CancellationToken cancellationToken);
    }
}
