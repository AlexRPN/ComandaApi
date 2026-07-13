using Comanda.DataTransfer.GruposAdicionais.Request;
using Comanda.DataTransfer.ProdutosVariacoes.Request;

namespace Comanda.DataTransfer.Produtos.Request
{
    public class ProdutoRequest
    {
        public int EmpresaId { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TempoPreparo { get; set; }
        public List<ProdutoVariacaoRequest> ProdutoVariacao { get; set; }
        //public List<IFormFile>? Imagens { get; set; }
        public List<GrupoAdicionalRequest>? GruposAdicionais { get; set; }

    }
}
