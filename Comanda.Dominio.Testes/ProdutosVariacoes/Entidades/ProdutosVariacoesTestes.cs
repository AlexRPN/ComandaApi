
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.ProdutosVariacoes.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.ProdutosVariacoes.Entidades
{
    public class ProdutosVariacoesTestes
    {
        private readonly ProdutoVariacao sut;
        public ProdutosVariacoesTestes()
        {
            sut = Builder<ProdutoVariacao>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new ProdutoVariacaoComando
                {
                    ProdutoId = 1,
                    Descricao = "Descrição teste",
                    Preco = 5.99m,
                    Ordem = 1,
                    Status = StatusEnum.Ativo,
                    DataCadastro = DateTime.Now.Date,
                    DataAlteracao = DateTime.Now.Date
                };

                var produtoVariacao = new ProdutoVariacao(comando);

                Assert.Equal(comando.ProdutoId, produtoVariacao.ProdutoId);
                Assert.Equal(comando.Descricao, produtoVariacao.Descricao);
                Assert.Equal(comando.Preco, produtoVariacao.Preco);
                Assert.Equal(comando.Ordem, produtoVariacao.Ordem);
                Assert.Equal(comando.Status, produtoVariacao.Status);
                Assert.Equal(comando.DataCadastro, produtoVariacao.DataCadastro);
            }
        }

        public class SetProdutoId : ProdutosVariacoesTestes
        {
            [Fact]
            public void Quando_ProdutoIdForValido_Espero_PropriedadeAtualizada()
            {
                var produtoId = sut.ProdutoId;

                sut.SetProdutoId(produtoId);

                Assert.Equal(produtoId, sut.ProdutoId);
            }
        }

        public class SetDescricao : ProdutosVariacoesTestes
        {
            [Fact]
            public void Quando_DescricaoForValida_Espero_PropriedadeAtualizada()
            {
                var descricao = sut.Descricao;

                sut.SetDescricao(descricao);

                Assert.Equal(descricao, sut.Descricao);
            }

            [Fact]
            public void Quando_DescricaoForNula_Espero_ArgumentException()
            {
                string descricao = null;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetDescricao(descricao));
                Assert.Equal("Descrição não pode ser nula ou vazia.", exception.Message);
            }

            [Fact]
            public void Quando_DescricaoForVazia_Espero_ArgumentException()
            {
                string descricao = "";

                var exception = Assert.Throws<ArgumentException>(() => sut.SetDescricao(descricao));
                Assert.Equal("Descrição não pode ser nula ou vazia.", exception.Message);
            }
        }

        public class SetPreco : ProdutosVariacoesTestes
        {
            [Fact]
            public void Quando_PrecoForValido_Espero_PropriedadeAtualizada()
            {
                var preco = sut.Preco;

                sut.SetPreco(preco);

                Assert.Equal(preco, sut.Preco);
            }

            [Fact]
            public void Quando_PrecoForNegativo_Espero_ArgumentException()
            {
                decimal preco = -12.99m;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetPreco(preco));
                Assert.Equal("Preço não pode ser negativo.", exception.Message);
            }
        }

        public class SetOrdem : ProdutosVariacoesTestes
        {
            [Fact]
            public void Quando_OrdemForValido_Espero_PropriedadeAtualizada()
            {
                var ordem = sut.Ordem;

                sut.SetOrdem(ordem);

                Assert.Equal(ordem, sut.Ordem);
            }
        }

        public class SetStatus : ProdutosVariacoesTestes
        {
            [Fact]
            public void Quando_StatusForValido_Espero_PropriedadeAtualizada()
            {
                var status = sut.Status;

                sut.SetStatus(status);

                Assert.Equal(status, sut.Status);
            }
        }

        public class SetDataCadastro : ProdutosVariacoesTestes
        {
            [Fact]
            public void Quando_DataCadastroForValido_Espero_PropriedadeAtualizada()
            {
                var dataCadastro = sut.DataCadastro;

                sut.SetDataCadastro(dataCadastro);

                Assert.Equal(dataCadastro, sut.DataCadastro);
            }

            [Fact]
            public void Quando_DataCadastroForAnteriorADataAtual_Espero_ArgumentException()
            {
                var dataCadastro = new DateTime(2026, 10, 27);

                var exception = Assert.Throws<ArgumentException>(() => sut.SetDataCadastro(dataCadastro));
                Assert.Equal("Data de cadastro não pode ser anterior à data atual.", exception.Message);
            }
        }

        public class SetDataAlteracao : ProdutosVariacoesTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValido_Espero_PropriedadeAtualizada()
            {
                var dataAlteracao = sut.DataAlteracao;

                sut.SetDataAlteracao(dataAlteracao);

                Assert.Equal(dataAlteracao, sut.DataAlteracao);
            }
        }
    }
}
