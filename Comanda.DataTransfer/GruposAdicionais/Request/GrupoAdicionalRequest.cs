using Comanda.DataTransfer.Adicionais.Request;

namespace Comanda.DataTransfer.GruposAdicionais.Request
{
    public class GrupoAdicionalRequest
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public List<AdicionalRequest> Adicionais { get; set; }
    }
}
