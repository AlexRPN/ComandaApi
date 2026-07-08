using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;

namespace Comanda.Dominio.Categorias.Servicos.Interfaces
{
    public interface ICategoriaServico
    {
        Task<Categoria> InserirAsync(CategoriaComando comando, CancellationToken cancellationToken);
        Task<Categoria> EditarAsync(CategoriaEditarComando comando, CancellationToken cancellationToken);
    }
}
