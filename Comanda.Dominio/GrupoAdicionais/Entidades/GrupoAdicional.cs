using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.Produtos.Entidades;

namespace Comanda.Dominio.GrupoAdicionais.Entidades
{
    public class GrupoAdicional
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1xN com Produto
        public int ProdutoId { get; private set; }
        public Produto Produto { get; private set; }

        // Relacionamento 1xN com Adicional
        public ICollection<Adicional> Adicionais { get; private set; }
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }

        private GrupoAdicional()
        {

        }

        public GrupoAdicional(GrupoAdicionalComando comando)
        {
            SetProdutoId(comando.ProdutoId);
            SetNome(comando.Nome);
        }

        public void SetProdutoId(int produtoId)
        {
            ProdutoId = produtoId;
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException("O Adicional não pode ser nulo ou vazio!");
            }

            Nome = nome;
        }
    }
}
