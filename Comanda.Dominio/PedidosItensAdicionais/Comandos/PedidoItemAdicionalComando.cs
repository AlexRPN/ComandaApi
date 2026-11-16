
namespace Comanda.Dominio.PedidosItensAdicionais.Comandos
{
    public class PedidoItemAdicionalComando
    {
        public int PedidoItemId { get; set; }
        public int AdicionalId { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
