using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;

namespace Comanda.Dominio.Usuarios.Servicos.Interfaces
{
    public interface IUsuarioServico
    {
        Task<Usuario> InserirAsync(UsuarioInserirComando comando, CancellationToken cancellationToken);
    }
}
