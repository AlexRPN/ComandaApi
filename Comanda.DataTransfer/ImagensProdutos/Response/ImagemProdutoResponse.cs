
namespace Comanda.DataTransfer.ImagensProdutos.Response
{
    public class ImagemProdutoResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string CaminhoArquivo { get; set; }
        public string UrlImagem { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
