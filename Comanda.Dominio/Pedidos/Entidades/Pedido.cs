
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Mesas.Entidades;
using Comanda.Dominio.Pedidos.Comandos;
using Comanda.Dominio.PedidosItens.Entidades;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Pedidos.Entidades
{
    public class Pedido
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com empresa
        public Empresa Empresa { get; private set; }
        public int EmpresaId { get; private set; }

        // Relacionamento 1:N com Cliente
        public Cliente Cliente { get; private set; }
        public int ClienteId { get; private set; }

        // Relacionamento 1:N com mesa
        public Mesa? Mesa { get; private set; }
        public int? MesaId { get; private set; }

        // Relacionamento 1:N com Usuário (Garçom)
        public Usuario? Usuario { get; private set; }
        public int? UsuarioId { get; private set; }

        // Relacionamento 1:N com PedidoItem
        public ICollection<PedidoItem> PedidoItem { get; set; }
        #endregion

        public int Id { get; private set; }
        public TipoPedidoEnum TipoPedido { get; private set; }
        public SituacaoPedidoEnum Situacao { get; private set; }
        public decimal ValorProduto { get; private set; }
        public decimal ValorEntrega { get; private set; }
        public decimal? ValorTaxaServico { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string? Observacao { get; private set; }
        public DateTime DataPedido { get; private set; }
        public DateTime? PrevisaoEntrega { get; private set; }

        private Pedido(){ }

        public Pedido(PedidoComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetClienteId(comando.ClienteId);
            SetMesaId(comando.MesaId);
            SetUsuarioId(comando.UsuarioId);
            SetTipoPedido(comando.TipoPedido);
            SetSituacao(comando.Situacao);
            SetValorProduto(comando.ValorProduto);
            SetValorEntrega(comando.ValorEntrega);
            SetValorTaxaServico(comando.ValorTaxaServico);
            SetValorTotal(comando.ValorTotal);
            SetObservacao(comando.Observacao);
            SetDataPedido(comando.DataPedido);
            SetPrevisaoEntrega(comando.PrevisaoEntrega);

        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetClienteId(int clienteId)
        {
            ClienteId = clienteId;
        }

        public void SetMesaId(int? mesaId)
        {
            MesaId = mesaId;
        }

        public void SetUsuarioId(int? usuarioId)
        {
            UsuarioId = usuarioId;
        }

        public void SetTipoPedido(TipoPedidoEnum tipoPedido)
        {
            TipoPedido = tipoPedido;
        }

        public void SetSituacao(SituacaoPedidoEnum situacao)
        {
            Situacao = situacao;
        }

        public void SetValorProduto(decimal valorProduto)
        {
            if(valorProduto <= 0)
            {
                throw new Exception("O valor do produto não pode ser negativo ou igual a zero!");
            }

            ValorProduto = valorProduto;
        }

        public void SetValorEntrega(decimal valorEntrega)
        {
            if(valorEntrega < 0)
            {
                throw new Exception("O valor da entrega não pode ser negativo!");
            }

            ValorEntrega = valorEntrega;
        }

        public void SetValorTaxaServico(decimal? valorTaxaServico)
        {
            if (valorTaxaServico < 0)
            {
                throw new Exception("O valor da taxa de serviço não pode ser negativo!");
            }

            ValorTaxaServico = valorTaxaServico;
        }

        public void SetValorTotal(decimal valorTotal)
        {
            if (valorTotal <= 0)
            {
                throw new Exception("O valor total do pedido não pode ser menor ou igual a zero!");
            }

            ValorTotal = valorTotal;
        }

        public void SetObservacao(string? observacao)
        {
            Observacao = observacao;
        }

        public void SetDataPedido(DateTime dataPedido)
        {
            DataPedido = dataPedido;
        }

        public void SetPrevisaoEntrega(DateTime? previsaoEntrega)
        {
            PrevisaoEntrega = previsaoEntrega;
        }
    }
}
