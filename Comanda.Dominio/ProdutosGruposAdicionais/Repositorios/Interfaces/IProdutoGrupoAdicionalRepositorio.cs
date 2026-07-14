using Comanda.Dominio.Genericos;
using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;

namespace Comanda.Dominio.ProdutosGruposAdicionais.Repositorios.Interfaces
{
    public interface IProdutoGrupoAdicionalRepositorio : IGenericoRepositorio<ProdutoGrupoAdicional>
    {
        Task<ProdutoGrupoAdicional> InserirAsync(ProdutoGrupoAdicionalComando comando, CancellationToken cancellationToken);
    }
}
