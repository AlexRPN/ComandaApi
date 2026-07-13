using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.ProdutosGruposAdicionais.Comandos;

namespace Comanda.Dominio.ProdutosGruposAdicionais.Entidades
{
    public class ProdutoGrupoAdicional
    {
        public int Id { get; private set; }

        public int ProdutoId { get; private set; }
        public Produto Produto { get; private set; }

        public int GrupoAdicionalId { get; private set; }
        public GrupoAdicional GrupoAdicional { get; private set; }

        private ProdutoGrupoAdicional()
        {
            
        }

        public ProdutoGrupoAdicional(ProdutoGrupoAdicionalComando comando)
        {
            SetProdutoId(comando.ProdutoId);
            SetGrupoAdicionalId(comando.GrupoAdicionalId);
        }

        public void SetProdutoId(int produtoId)
        {
            ProdutoId = produtoId;
        }

        public void SetGrupoAdicionalId(int grupoAdicionalId)
        {
            GrupoAdicionalId = grupoAdicionalId;
        }
    }
}
