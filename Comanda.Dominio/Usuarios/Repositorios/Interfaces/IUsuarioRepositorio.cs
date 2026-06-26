using Comanda.Dominio.Genericos;
using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;

namespace Comanda.Dominio.Usuarios.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IGenericoRepositorio<Usuario>
    {
        Task<Usuario> InserirAsync(UsuarioComando comando, CancellationToken cancellationToken);
    }
}
