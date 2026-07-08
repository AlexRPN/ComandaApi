using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.ProdutosVariacoes.Comandos
{
    public class ProdutoVariacaoComando
    {
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        // Controla a ordem de exibição das opções do produto.
        public int Ordem { get; set; }
        public AtivoInativoEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
