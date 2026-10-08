using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Produtos.Comandos
{
    public class ProdutoComando
    {
        public int EmpresaId { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TempoPreparo { get; set; }
        public StatusEnum Status { get; set; }
        public SituacaoProdutoEnum SituacaoProduto { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
