using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.Categorias.Repositorios
{
    public class CategoriaRepositorio : GenericoRepositorio<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Categoria> InserirAsync(CategoriaComando comando, CancellationToken cancellationToken)
        {
            var categoria = new Categoria(comando);

            await appDbContext.Categorias.AddAsync(categoria, cancellationToken);

            return categoria;
        }
    }
}
