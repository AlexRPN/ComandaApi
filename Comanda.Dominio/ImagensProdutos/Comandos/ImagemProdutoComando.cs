
namespace Comanda.Dominio.ImagensProdutos.Comandos
{
    public class ImagemProdutoComando
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string CaminhoArquivo { get; set; }
        public string UrlImagem { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
