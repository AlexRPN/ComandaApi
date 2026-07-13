using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Produtos.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

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
    }
}
