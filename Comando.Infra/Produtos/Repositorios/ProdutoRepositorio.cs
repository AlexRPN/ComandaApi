using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Filtros;
using Comanda.Dominio.Produtos.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Produtos.Repositorios
{
    public class ProdutoRepositorio : GenericoRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Produto> InserirAsync(ProdutoComando comando, CancellationToken cancellationToken)
        {
            Produto produto = new Produto(comando);

            await appDbContext.Produtos.AddAsync(produto, cancellationToken);

            return produto;
        }

        public Task<IQueryable<Produto>> FiltrarAsync(ProdutoListarFiltro filtro, CancellationToken cancellationToken)
        {
            IQueryable<Produto> query = appDbContext.Produtos
                                                    .AsNoTracking()
                                                    .AsQueryable();

            if (filtro.Id.HasValue)
            {
                query = query.Where(p => p.Id == filtro.Id.Value);
            }
            if(filtro.EmpresaId.HasValue)
            {
                query = query.Where(p => p.EmpresaId == filtro.EmpresaId.Value);
            }
            if (filtro.CategoriaId.HasValue)
            {
                query = query.Where(p => p.CategoriaId == filtro.CategoriaId.Value);
            }
            if(!string.IsNullOrEmpty(filtro.Nome))
            {
                query = query.Where(p => p.Nome.Contains(filtro.Nome));
            }
            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                query = query.Where(p => p.Descricao.Contains(filtro.Descricao));
            }
            if (filtro.Status.HasValue)
            {
                query = query.Where(p => p.Status == filtro.Status.Value);
            }

            return Task.FromResult(query);
        }
    }
}
