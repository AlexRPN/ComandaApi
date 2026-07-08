using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Categorias.Repositorios.Filtros;
using Comanda.Dominio.Categorias.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Categorias.Repositorios
{
    public class CategoriaRepositorio : GenericoRepositorio<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public Task<IQueryable<Categoria>> FiltrarAsync(CategoriaListarFiltro filtro, CancellationToken cancellationToken)
        {
            IQueryable<Categoria> query = appDbContext.Categorias
                                                      .AsNoTracking()
                                                      .AsQueryable();

            if(filtro.Id.HasValue)
            {
                query = query.Where(c => c.Id == filtro.Id.Value);
            }

            if(filtro.EmpresaId.HasValue)
            {
                query = query.Where(c => c.EmpresaId == filtro.EmpresaId.Value);
            }

            if(!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                query = query.Where(c => c.Nome.Contains(filtro.Nome));
            }

            if(filtro.DataCadastro.HasValue)
            {
                query = query.Where(c => c.DataCadastro.Date == filtro.DataCadastro.Value.Date);
            }

            return Task.FromResult(query);
        }

        public async Task<Categoria> InserirAsync(CategoriaComando comando, CancellationToken cancellationToken)
        {
            var categoria = new Categoria(comando);

            await appDbContext.Categorias.AddAsync(categoria, cancellationToken);

            return categoria;
        }
    }
}
