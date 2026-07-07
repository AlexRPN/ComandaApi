using Comanda.Dominio.ImagensProdutos.Comandos;
using Comanda.Dominio.Produtos.Entidades;

namespace Comanda.Dominio.ImagensProdutos.Entidades
{
    public class ImagemProduto
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Produto
        public Produto Produto { get; private set; }
        public int ProdutoId { get; private set; }
        #endregion

        public int Id { get; private set; }
        public string CaminhoArquivo { get; private set; }
        public string UrlImagem { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }

        private ImagemProduto()
        {
            
        }

        public ImagemProduto(ImagemProdutoComando comando)
        {
           SetProdutoId(comando.ProdutoId);
           SetCaminhoArquivo(comando.CaminhoArquivo);
           SetUrlImagem(comando.UrlImagem);
           SetDataCadastro(comando.DataCadastro);
           SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetProdutoId(int produtoId)
        {
            ProdutoId = produtoId;
        }

        public void SetCaminhoArquivo(string caminhoArquivo)
        {
            CaminhoArquivo = caminhoArquivo;
        }

        public void SetUrlImagem(string urlImagem)
        {
            UrlImagem = urlImagem;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            DataCadastro = dataCadastro;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            DataAlteracao = dataAlteracao;
        }
    }
}
