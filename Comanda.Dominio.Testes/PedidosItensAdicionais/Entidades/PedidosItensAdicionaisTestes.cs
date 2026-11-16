
using Comanda.Dominio.PedidosItens.Comandos;
using Comanda.Dominio.PedidosItensAdicionais.Comandos;
using Comanda.Dominio.PedidosItensAdicionais.Entidades;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Testes.PedidosItens.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.PedidosItensAdicionais.Entidades
{
    public class PedidosItensAdicionaisTestes
    {
        private readonly PedidoItemAdicional sut;
        public PedidosItensAdicionaisTestes()
        {
            sut = Builder<PedidoItemAdicional>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new PedidoItemAdicionalComando()
                {
                    PedidoItemId = 1,
                    AdicionalId = 1,
                    Quantidade = 1,
                    Valor = 33.90m
                };

                var pedidoItemAdicional = new PedidoItemAdicional(comando);

                Assert.Equal(comando.PedidoItemId, pedidoItemAdicional.PedidoItemId);
                Assert.Equal(comando.AdicionalId, pedidoItemAdicional.AdicionalId);
                Assert.Equal(comando.Quantidade, pedidoItemAdicional.Quantidade);
                Assert.Equal(comando.Valor, pedidoItemAdicional.Valor);
            }
        }

        public class SetPedidoItemId : PedidosItensAdicionaisTestes
        {
            [Fact]
            public void Quando_PedidoItemIdForValido_Espero_PropriedadeAtualizada()
            {
                var pedidoItemId = 1;

                sut.SetPedidoItemId(pedidoItemId);

                Assert.Equal(pedidoItemId, sut.PedidoItemId);
            }
        }

        public class SetAdicionalId : PedidosItensAdicionaisTestes
        {
            [Fact]
            public void Quando_AdicionalIdForValido_Espero_PropriedadeAtualizada()
            {
                var adicionalId = 1;

                sut.SetAdicionalId(adicionalId);

                Assert.Equal(adicionalId, sut.AdicionalId);
            }
        }

        public class SetQuantidade : PedidosItensAdicionaisTestes
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
                Assert.Equal("A quantidade não pode ser negativa!", exception.Message);
            }
        }

        public class SetValor : PedidosItensAdicionaisTestes
        {
            [Fact]
            public void Quando_ValorForValido_Espero_PropriedadeAtualizada()
            {
                var valor = 39.90m;

                sut.SetValor(valor);

                Assert.Equal(valor, sut.Valor);
            }

            [Fact]
            public void Quando_ValorForNegativo_Espero_Exception()
            {
                var valor = -3;

                var exception = Assert.Throws<Exception>(() => sut.SetValor(valor));
                Assert.Equal("O valor não pode ser negativo!", exception.Message);
            }
        }
    }
}
