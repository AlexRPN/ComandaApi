using Comanda.DataTransfer.Adicionais.Response;

namespace Comanda.DataTransfer.GruposAdicionais.Response
{
    public class GrupoAdicionalResponse
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public List<AdicionalResponse> Adicionais { get; set; }
    }
}
