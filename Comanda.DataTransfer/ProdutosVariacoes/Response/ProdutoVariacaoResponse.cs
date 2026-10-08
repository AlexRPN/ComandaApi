using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.ProdutosVariacoes.Response
{
    public class ProdutoVariacaoResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Ordem { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
