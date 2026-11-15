
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Pedidos.Comandos
{
    public class PedidoComando
    {
        public int EmpresaId { get; set; }
        public int ClienteId { get; set; }
        public int? MesaId { get; set; }
        public int? UsuarioId { get; set; }
        public TipoPedidoEnum TipoPedido { get; set; }
        public SituacaoPedidoEnum Situacao { get; set; }
        public decimal ValorProduto { get; set; }
        public decimal ValorEntrega { get; set; }
        public decimal? ValorTaxaServico { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime? PrevisaoEntrega { get; set; }
    }
}
