using Comanda.DataTransfer.Usuarios.Request;
using Comanda.DataTransfer.Usuarios.Response;

namespace Comanda.Aplicacao.Usuarios.Servicos.Interfaces
{
    public interface IUsuarioAppServico
    {
        Task<UsuarioResponse> InserirAsync(UsuarioRequest request, CancellationToken cancellationToken);
    }
}
