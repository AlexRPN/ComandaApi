using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.ProdutosVariacoes.Entidades
{
    public class ProdutoVariacao
    {
        #region Navegação com os relacionamentos
        // Relacionamento N:N com Produto
        public Produto Produto { get; private set; }
        public int ProdutoId { get; private set; }
        #endregion

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int Ordem { get; private set; }
        public AtivoInativoEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }

        private ProdutoVariacao()
        {

        }

        public ProdutoVariacao(ProdutoVariacaoComando comando)
        {
            SetProdutoId(comando.ProdutoId);
            SetDescricao(comando.Descricao);
            SetPreco(comando.Preco);
            SetOrdem(comando.Ordem);
            SetStatus(comando.Status);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetProdutoId(int produtoId)
        {
            ProdutoId = produtoId;
        }

        public void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentException("Descrição não pode ser nula ou vazia.");
            }

            Descricao = descricao;
        }

        public void SetPreco(decimal preco)
        {
            if (preco < 0)
            {
                throw new ArgumentException("Preço não pode ser negativo.");
            }

            Preco = preco;
        }

        public void SetOrdem(int ordem)
        {
            Ordem = ordem;
        }

        public void SetStatus(AtivoInativoEnum status)
        {
            Status = status;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            if (dataCadastro < DateTime.Now)
            {
                throw new ArgumentException("Data de cadastro não pode ser anterior à data atual.");
            }

            DataCadastro = dataCadastro;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            if (dataAlteracao < DataCadastro)
            {
                throw new ArgumentException("Data de alteração não pode ser anterior à data de cadastro.");
            }

            DataAlteracao = dataAlteracao;
        }
    }
}
