
using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.PedidosItens.Entidades;
using Comanda.Dominio.PedidosItensAdicionais.Comandos;

namespace Comanda.Dominio.PedidosItensAdicionais.Entidades
{
    public class PedidoItemAdicional
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com PedidoItem
        public int PedidoItemId { get; private set; }
        public PedidoItem PedidoItem { get; private set; }

        // Relacionamento 1:N com Adicional
        public int AdicionalId { get; private set; }
        public Adicional Adicional { get; private set; }
        #endregion

        public int Id { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Valor { get; private set; }

        private PedidoItemAdicional()
        {
            
        }

        public PedidoItemAdicional(PedidoItemAdicionalComando comando)
        {
            SetPedidoItemId(comando.PedidoItemId);
            SetAdicionalId(comando.AdicionalId);
            SetQuantidade(comando.Quantidade);
            SetValor(comando.Valor);
        }

        public void SetPedidoItemId(int pedidoItemId)
        {
            PedidoItemId = pedidoItemId;
        }

        public void SetAdicionalId(int adicionalId)
        {
            AdicionalId = adicionalId;
        }

        public void SetQuantidade(int quantidade)
        {
            if(quantidade < 0)
            {
                throw new Exception("A quantidade não pode ser negativa!");
            }

            Quantidade = quantidade;
        }

        public void SetValor(decimal valor)
        {
            if(valor < 0)
            {
                throw new Exception("O valor não pode ser negativo!");
            }

            Valor = valor;
        }
    }
}
