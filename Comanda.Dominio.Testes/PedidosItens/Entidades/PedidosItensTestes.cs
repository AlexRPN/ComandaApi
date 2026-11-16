
using Comanda.Dominio.PedidosItens.Comandos;
using Comanda.Dominio.PedidosItens.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.PedidosItens.Entidades
{
    public class PedidosItensTestes
    {
        private readonly PedidoItem sut;
        public PedidosItensTestes()
        {
            sut = Builder<PedidoItem>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new PedidoItemComando()
                {
                    PedidoId = 1,
                    ProdutoId = 1,
                    Quantidade = 3,
                    ValorUnitario = 3.0m,
                    ValorTotal = 9.0m,
                    Observacao = "Observação teste"
                };

                var PedidoItem = new PedidoItem(comando);

                Assert.Equal(comando.PedidoId, PedidoItem.PedidoId);
                Assert.Equal(comando.ProdutoId, PedidoItem.ProdutoId);
                Assert.Equal(comando.Quantidade, PedidoItem.Quantidade);
                Assert.Equal(comando.ValorUnitario, PedidoItem.ValorUnitario);
                Assert.Equal(comando.ValorTotal, PedidoItem.ValorTotal);
                Assert.Equal(comando.Observacao, PedidoItem.Observacao);
            }
        }

        public class SetPedidoId : PedidosItensTestes
        {
            [Fact]
            public void Quando_PedidoIdForValido_Espero_PropriedadeAtualizada()
            {
                var pedidoId = 1;

                sut.SetPedidoId(pedidoId);

                Assert.Equal(pedidoId, sut.PedidoId);
            }
        }

        public class SetProdutoId : PedidosItensTestes
        {
            [Fact]
            public void Quando_ProdutoIdForValido_Espero_PropriedadeAtualizada()
            {
                var produtoId = 1;

                sut.SetProdutoId(produtoId);

                Assert.Equal(produtoId, sut.ProdutoId);
            }
        }

        public class SetQuantidade : PedidosItensTestes
        {
            [Fact]
            public void Quando_QuantidadeForValido_Espero_PropriedadeAtualizada()
            {
                var quantidade = 3;

                sut.SetQuantidade(quantidade);

                Assert.Equal(quantidade, sut.Quantidade);
            }

            [Fact]
            public void Quando_QuantidadeForNegativo_Espero_Exception()
            {
                var quantidade = -3;

                var exception = Assert.Throws<Exception>(() => sut.SetQuantidade(quantidade));
                Assert.Equal("A quantidade não pode ser negativa ou igual a zero!", exception.Message);
            }

            [Fact]
            public void Quando_QuantidadeForZero_Espero_Exception()
            {
                var quantidade = 0;

                var exception = Assert.Throws<Exception>(() => sut.SetQuantidade(quantidade));
                Assert.Equal("A quantidade não pode ser negativa ou igual a zero!", exception.Message);
            }
        }

        public class SetValorUnitario : PedidosItensTestes
        {
            [Fact]
            public void Quando_ValorUnitarioForValido_Espero_PropriedadeAtualizada()
            {
                var valorUnitario = 3.99m;

                sut.SetValorUnitario(valorUnitario);

                Assert.Equal(valorUnitario, sut.ValorUnitario);
            }

            [Fact]
            public void Quando_ValorUnitarioForNegativo_Espero_Exception()
            {
                var valorUnitario = -3.99m;

                var exception = Assert.Throws<Exception>(() => sut.SetValorUnitario(valorUnitario));
                Assert.Equal("O valor unitário não pode ser negativo ou igual a zero!", exception.Message);
            }

            [Fact]
            public void Quando_ValorUnitarioForZero_Espero_Exception()
            {
                var valorUnitario = 0;

                var exception = Assert.Throws<Exception>(() => sut.SetValorUnitario(valorUnitario));
                Assert.Equal("O valor unitário não pode ser negativo ou igual a zero!", exception.Message);
            }
        }

        public class SetValorTotal : PedidosItensTestes
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
                Assert.Equal("O valor total não pode ser negativo ou igual a zero!", exception.Message);
            }

            [Fact]
            public void Quando_ValorTotalForZero_Espero_Exception()
            {
                var valorTotal = 0;

                var exception = Assert.Throws<Exception>(() => sut.SetValorTotal(valorTotal));
                Assert.Equal("O valor total não pode ser negativo ou igual a zero!", exception.Message);
            }
        }

        public class SetObservacao : PedidosItensTestes
        {
            [Fact]
            public void Quando_ObservacaoForValido_Espero_PropriedadeAtualizada()
            {
                var observacao = "Observacao teste!";

                sut.SetObservacao(observacao);

                Assert.Equal(observacao, sut.Observacao);
            }
        }
    }
}
