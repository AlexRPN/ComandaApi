
using Comanda.Dominio.Pedidos.Comandos;
using Comanda.Dominio.Pedidos.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Pedidos.Entidades
{
    public class PedidosTestes
    {
        private readonly Pedido sut;
        public PedidosTestes()
        {
            sut = Builder<Pedido>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new PedidoComando
                {
                    EmpresaId = 1,
                    ClienteId = 1,
                    MesaId = 1,
                    UsuarioId = 1,
                    TipoPedido = TipoPedidoEnum.Mesa,
                    Situacao = SituacaoPedidoEnum.Confirmado,
                    ValorProduto = 15.50m,
                    ValorEntrega = 5.90m,
                    ValorTaxaServico = 7.50m,
                    ValorTotal = 38.86m,
                    Observacao = "Observação teste",
                    DataPedido = DateTime.Now,
                    PrevisaoEntrega = DateTime.Now.AddMinutes(30),
                };

                var pedido = new Pedido(comando);

                Assert.Equal(comando.EmpresaId, pedido.EmpresaId);
                Assert.Equal(comando.ClienteId, pedido.ClienteId);
                Assert.Equal(comando.MesaId, pedido.MesaId);
                Assert.Equal(comando.UsuarioId, pedido.UsuarioId);
                Assert.Equal(comando.TipoPedido, pedido.TipoPedido);
                Assert.Equal(comando.Situacao, pedido.Situacao);
                Assert.Equal(comando.ValorProduto, pedido.ValorProduto);
                Assert.Equal(comando.ValorEntrega, pedido.ValorEntrega);
                Assert.Equal(comando.ValorTaxaServico, pedido.ValorTaxaServico);
                Assert.Equal(comando.ValorTotal, pedido.ValorTotal);
                Assert.Equal(comando.Observacao, pedido.Observacao);
                Assert.Equal(comando.DataPedido, pedido.DataPedido);
                Assert.Equal(comando.PrevisaoEntrega, pedido.PrevisaoEntrega);
            }
        }

        public class SetEmpresaId : PedidosTestes
        {
            [Fact]
            public void Quando_EmpresaIdForValido_Espero_PropriedadeAtualizada()
            {
                var empresaId = 1;

                sut.SetEmpresaId(empresaId);

                Assert.Equal(empresaId, sut.EmpresaId);
            }
        }

        public class SetClienteId : PedidosTestes
        {
            [Fact]
            public void Quando_ClienteIdForValido_Espero_PropriedadeAtualizada()
            {
                var clienteId = 1;

                sut.SetClienteId(clienteId);

                Assert.Equal(clienteId, sut.ClienteId);
            }
        }

        public class SetMesaId : PedidosTestes
        {
            [Fact]
            public void Quando_MesaIdForValido_Espero_PropriedadeAtualizada()
            {
                var mesaId = 1;

                sut.SetMesaId(mesaId);

                Assert.Equal(mesaId, sut.MesaId);
            }
        }

        public class SetUsuarioId : PedidosTestes
        {
            [Fact]
            public void Quando_UsuarioIdForValido_Espero_PropriedadeAtualizada()
            {
                var usuarioId = 1;

                sut.SetUsuarioId (usuarioId);

                Assert.Equal(usuarioId, sut.UsuarioId);
            }
        }

        public class SetTipoPedido : PedidosTestes
        {
            [Fact]
            public void Quando_TipoPedidoForValido_Espero_PropriedadeAtualizada()
            {
                var tipoPedido = TipoPedidoEnum.Mesa;

                sut.SetTipoPedido(tipoPedido);

                Assert.Equal(tipoPedido, sut.TipoPedido);
            }
        }

        public class SetSituacao : PedidosTestes
        {
            [Fact]
            public void Quando_SituacaoForValido_Espero_PropriedadeAtualizada()
            {
                var situacao = SituacaoPedidoEnum.Preparando;

                sut.SetSituacao(situacao);

                Assert.Equal(situacao, sut.Situacao);
            }
        }

        public class SetValorProduto : PedidosTestes
        {
            [Fact]
            public void Quando_ValorProdutoForValido_Espero_PropriedadeAtualizada()
            {
                var valorProduto = 36.90m;

                sut.SetValorProduto(valorProduto);

                Assert.Equal(valorProduto, sut.ValorProduto);
            }

            [Fact]
            public void Quando_ValorProdutoForNegativo_Espero_Exception()
            {
                var valorProduto = -15.50m;

                var exception = Assert.Throws<Exception>(() => sut.SetValorProduto(valorProduto));
                Assert.Equal("O valor do produto não pode ser negativo ou igual a zero!", exception.Message);
            }

            [Fact]
            public void Quando_ValorProdutoForZero_Espero_Exception()
            {
                var valorProduto = 0;

                var exception = Assert.Throws<Exception>(() => sut.SetValorProduto(valorProduto));
                Assert.Equal("O valor do produto não pode ser negativo ou igual a zero!", exception.Message);
            }
        }

        public class SetValorEntrega : PedidosTestes
        {
            [Fact]
            public void Quando_ValorEntregaForValido_Espero_PropriedadeAtualizada()
            {
                var valorEntrega = 7.55m;

                sut.SetValorEntrega(valorEntrega);

                Assert.Equal(valorEntrega, sut.ValorEntrega);
            }

            [Fact]
            public void Quando_ValorEntregaForNegativo_Espero_Exception()
            {
                var valorEntrega = -7.50m;

                var exception = Assert.Throws<Exception>(() => sut.SetValorEntrega(valorEntrega));
                Assert.Equal("O valor da entrega não pode ser negativo!", exception.Message);
            }
        }

        public class SetValorTaxaServico : PedidosTestes
        {
            [Fact]
            public void Quando_ValorTaxaServicoForValido_Espero_PropriedadeAtualizada()
            {
                var valorTaxaServico = 7.55m;

                sut.SetValorTaxaServico(valorTaxaServico);

                Assert.Equal(valorTaxaServico, sut.ValorTaxaServico);
            }

            [Fact]
            public void Quando_ValorTaxaServicoForNegativo_Espero_Exception()
            {
                var valorTaxaServico = -7.50m;

                var exception = Assert.Throws<Exception>(() => sut.SetValorTaxaServico(valorTaxaServico));
                Assert.Equal("O valor da taxa de serviço não pode ser negativo!", exception.Message);
            }
        }

        public class SetValorTotal : PedidosTestes
        {
            [Fact]
            public void Quando_ValorTotalForValido_Espero_PropriedadeAtualizada()
            {
                var valorTotal = 55.60m;

                sut.SetValorTotal(valorTotal);

                Assert.Equal(valorTotal, sut.ValorTotal);
            }

            [Fact]
            public void Quando_ValorTotalForNegativo_Espero_Exception()
            {
                var valorTotal = -15.50m;

                var exception = Assert.Throws<Exception>(() => sut.SetValorTotal(valorTotal));
                Assert.Equal("O valor total do pedido não pode ser menor ou igual a zero!", exception.Message);
            }

            [Fact]
            public void Quando_ValorTotalForZero_Espero_Exception()
            {
                var valorTotal = 0;

                var exception = Assert.Throws<Exception>(() => sut.SetValorTotal(valorTotal));
                Assert.Equal("O valor total do pedido não pode ser menor ou igual a zero!", exception.Message);
            }
        }

        public class SetObservacao : PedidosTestes
        {
            [Fact]
            public void Quando_ObservacaoForValido_Espero_PropriedadeAtualizada()
            {
                var observacao = "Observacao teste!";

                sut.SetObservacao(observacao);

                Assert.Equal(observacao, sut.Observacao);
            }
        }

        public class SetDataPedido : PedidosTestes
        {
            [Fact]
            public void Quando_DataPedidoForValido_Espero_PropriedadeAtualizada()
            {
                var dataPedido = DateTime.Now;

                sut.SetDataPedido(dataPedido);

                Assert.Equal(dataPedido, sut.DataPedido);
            }
        }

        public class SetPrevisaoEntrega : PedidosTestes
        {
            [Fact]
            public void Quando_PrevisaoEntregaForValido_Espero_PropriedadeAtualizada()
            {
                var previsaoEntrega = DateTime.Now.AddMinutes(30);

                sut.SetPrevisaoEntrega(previsaoEntrega);

                Assert.Equal(previsaoEntrega, sut.PrevisaoEntrega);
            }
        }
    }
}
