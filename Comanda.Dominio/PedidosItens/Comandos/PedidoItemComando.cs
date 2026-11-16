
namespace Comanda.Dominio.PedidosItens.Comandos
{
    public class PedidoItemComando
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public string? Observacao { get; set; }
    }
}
