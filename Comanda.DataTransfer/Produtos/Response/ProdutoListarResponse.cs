using Comanda.DataTransfer.GruposAdicionais.Response;
using Comanda.DataTransfer.ImagensProdutos.Response;
using Comanda.DataTransfer.ProdutosVariacoes.Response;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Produtos.Response
{
    public class ProdutoListarResponse
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TempoPreparo { get; set; }
        public StatusEnum Status { get; set; }
        public SituacaoProdutoEnum SituacaoProduto { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<ProdutoVariacaoResponse> ProdutoVariacao { get; set; }
        public List<ImagemProdutoResponse> ImagensProdutos { get; set; }
    }
}
