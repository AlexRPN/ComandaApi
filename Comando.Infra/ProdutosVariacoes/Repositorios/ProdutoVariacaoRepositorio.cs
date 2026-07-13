using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Entidades;
using Comanda.Dominio.ProdutosVariacoes.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.ProdutosVariacoes.Repositorios
{
    public class ProdutoVariacaoRepositorio : GenericoRepositorio<ProdutoVariacao>, IProdutoVariacaoRepositorio
    {
        public ProdutoVariacaoRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<ProdutoVariacao>> InserirAsync(List<ProdutoVariacaoComando> comandos, CancellationToken cancellationToken)
        {
            var produtoVariacoes = comandos.Select(comando => new ProdutoVariacao(comando)).ToList();

            await appDbContext.ProdutosVariacoes.AddRangeAsync(produtoVariacoes, cancellationToken);

            return produtoVariacoes;
        }
    }
}
