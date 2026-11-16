
using Comanda.Dominio.Pedidos.Entidades;
using Comanda.Dominio.PedidosItens.Comandos;
using Comanda.Dominio.PedidosItensAdicionais.Entidades;
using Comanda.Dominio.Produtos.Entidades;

namespace Comanda.Dominio.PedidosItens.Entidades
{
    public class PedidoItem
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Pedido
        public Pedido Pedido { get; private set; }
        public int PedidoId { get; private set; }

        // Relacionamento 1:N com Produto
        public Produto Produto { get; private set; }
        public int ProdutoId { get; private set; }

        // Relacionamento 1:N com PedidoItemAdicional
        public ICollection<PedidoItemAdicional> Adicionais { get; private set; }
        #endregion

        public int Id { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string? Observacao { get; private set; }

        private PedidoItem()
        {
                    
        }

        public PedidoItem(PedidoItemComando comando)
        {
            SetPedidoId(comando.PedidoId);
            SetProdutoId(comando.ProdutoId);
            SetQuantidade(comando.Quantidade);
            SetValorUnitario(comando.ValorUnitario);
            SetValorTotal(comando.ValorTotal);
            SetObservacao(comando.Observacao);
        }

        public void SetPedidoId(int pedidoId)
        {
            PedidoId = pedidoId;
        }

        public void SetProdutoId(int produtoId)
        {
            ProdutoId = produtoId;
        }

        public void SetQuantidade(int quantidade)
        {
            if(quantidade <= 0)
            {
                throw new Exception("A quantidade não pode ser negativa ou igual a zero!");
            }

            Quantidade = quantidade;
        }

        public void SetValorUnitario(decimal valorUnitario)
        {
            if(valorUnitario <= 0)
            {
                throw new Exception("O valor unitário não pode ser negativo ou igual a zero!");
            }

            ValorUnitario = valorUnitario;
        }

        public void SetValorTotal(decimal valorTotal)
        {
            if(valorTotal <= 0)
            {
                throw new Exception("O valor total não pode ser negativo ou igual a zero!");
            }

            ValorTotal = valorTotal;
        }

        public void SetObservacao(string? observacao)
        {
            Observacao = observacao;
        }
    }
}
