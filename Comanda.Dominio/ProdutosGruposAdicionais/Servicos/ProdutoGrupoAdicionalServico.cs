using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;
using Comanda.Dominio.ProdutosGruposAdicionais.Repositorios.Interfaces;
using Comanda.Dominio.ProdutosGruposAdicionais.Servicos.Interfaces;

namespace Comanda.Dominio.ProdutosGruposAdicionais.Servicos
{
    public class ProdutoGrupoAdicionalServico : IProdutoGrupoAdicionalServico
    {
        private readonly IProdutoGrupoAdicionalRepositorio produtoGrupoAdicionalRepositorio;
        public ProdutoGrupoAdicionalServico(IProdutoGrupoAdicionalRepositorio produtoGrupoAdicionalRepositorio)
        {
            this.produtoGrupoAdicionalRepositorio = produtoGrupoAdicionalRepositorio;
        }

        public async Task<ProdutoGrupoAdicional> InserirAsync(ProdutoGrupoAdicionalComando comando, CancellationToken cancellationToken)
        {
            return await produtoGrupoAdicionalRepositorio.InserirAsync(comando, cancellationToken);
        }
    }
}
