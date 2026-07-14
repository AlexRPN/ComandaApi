using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;
using Comanda.Dominio.ProdutosGruposAdicionais.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.ProdutosGruposAdicionais.Repositorios
{
    public class ProdutoGrupoAdicionalRepositorio : GenericoRepositorio<ProdutoGrupoAdicional>, IProdutoGrupoAdicionalRepositorio
    {
        public ProdutoGrupoAdicionalRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<ProdutoGrupoAdicional> InserirAsync(ProdutoGrupoAdicionalComando comando, CancellationToken cancellationToken)
        {
            ProdutoGrupoAdicional produtoGrupoAdicional = new ProdutoGrupoAdicional(comando);

            await appDbContext.ProdutosGruposAdicionais.AddAsync(produtoGrupoAdicional, cancellationToken);

            return produtoGrupoAdicional;
        }
    }
}
