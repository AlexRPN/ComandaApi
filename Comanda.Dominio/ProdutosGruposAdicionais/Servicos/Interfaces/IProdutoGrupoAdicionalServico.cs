using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;

namespace Comanda.Dominio.ProdutosGruposAdicionais.Servicos.Interfaces
{
    public interface IProdutoGrupoAdicionalServico
    {
        Task<ProdutoGrupoAdicional> InserirAsync(ProdutoGrupoAdicionalComando comando, CancellationToken cancellationToken);
    }
}
