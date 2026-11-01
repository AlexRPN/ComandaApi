using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Produtos.Entidades
{
    public class ProdutosTestes
    {
        private readonly Produto sut;
        public ProdutosTestes()
        {
            sut = Builder<Produto>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new ProdutoComando
                {
                    EmpresaId = 1,
                    CategoriaId = 1,
                    Nome = "Produto teste",
                    Descricao = "Descrição teste",
                    TempoPreparo = 10,
                    Status = StatusEnum.Ativo,
                    SituacaoProduto = SituacaoProdutoEnum.Disponivel,
                    DataCadastro = DateTime.Now.Date
                };

                var produto = new Produto(comando);

                Assert.Equal(comando.EmpresaId, produto.EmpresaId);
                Assert.Equal(comando.CategoriaId, produto.CategoriaId);
                Assert.Equal(comando.Nome, produto.Nome);
                Assert.Equal(comando.Descricao, produto.Descricao);
                Assert.Equal(comando.TempoPreparo, produto.TempoPreparo);
                Assert.Equal(comando.Status, produto.Status);
                Assert.Equal(comando.SituacaoProduto, produto.SituacaoProduto);
                Assert.Equal(comando.DataCadastro, produto.DataCadastro);
            }
        }

        public class SetSituacaoProduto : ProdutosTestes
        {
            [Fact]
            public void Quando_SituacaoProdutoForValido_Espero_PropriedadeAtualizada()
            {
                var situacaoProduto = SituacaoProdutoEnum.Disponivel;

                sut.SetSituacaoProduto(situacaoProduto);

                Assert.Equal(situacaoProduto, sut.SituacaoProduto);
            }
        }

        public class SetEmpresaId : ProdutosTestes
        {
            [Fact]
            public void Quando_EmpresaIdForValido_Espero_PropriedadeAtualizada()
            {
                var empresaId = 1;

                sut.SetEmpresaId(empresaId);

                Assert.Equal(empresaId, sut.EmpresaId);
            }
        }

        public class SetCategoriaId : ProdutosTestes
        {
            [Fact]
            public void Quando_CategoriaIdForValido_Espero_PropriedadeAtualizada()
            {
                var categoriaId = 1;

                sut.SetCategoriaId(categoriaId);

                Assert.Equal(categoriaId, sut.CategoriaId);
            }
        }

        public class SetNome : ProdutosTestes
        {
            [Fact]
            public void Quando_NomeProdutoForValido_Espero_PropriedadeAtualizada()
            {
                var nomeProduto = "Nome teste";

                sut.SetNome(nomeProduto);

                Assert.Equal(nomeProduto, sut.Nome);
            }

            [Fact]
            public void Quando_NomeProdutoForNulo_Espero_ArgumentException()
            {
                string nomeProduto = null;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(nomeProduto));
                Assert.Equal("O nome do produto não pode ser nulo ou vazio!", exception.Message);
            }

            [Fact]
            public void Quando_NomeProdutoForVazio_Espero_ArgumentException()
            {
                string nomeProduto = "";

                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(nomeProduto));
                Assert.Equal("O nome do produto não pode ser nulo ou vazio!", exception.Message);
            }
        }

        public class SetDescricao : ProdutosTestes
        {
            [Fact]
            public void Quando_DescricaoProdutoForValido_Espero_PropriedadeAtualizada()
            {
                var descricaoProduto = "Descricao teste";

                sut.SetDescricao(descricaoProduto);

                Assert.Equal(descricaoProduto, sut.Descricao);
            }

            [Fact]
            public void Quando_DescricaoProdutoForNulo_Espero_ArgumentException()
            {
                string descricaoProduto = null;

                var exception = Assert.Throws<ArgumentException>(() => sut.SetDescricao(descricaoProduto));

                Assert.Equal("A descrição do produto não pode ser nulo ou vazio!", exception.Message);
            }

            [Fact]
            public void Quando_DescricaoProdutoForVazio_Espero_ArgumentException()
            {
                string descricaoProduto = "";

                var exception = Assert.Throws<ArgumentException>(() => sut.SetDescricao(descricaoProduto));

                Assert.Equal("A descrição do produto não pode ser nulo ou vazio!", exception.Message);
            }
        }

        public class SetTempoPreparo : ProdutosTestes
        {
            [Fact]
            public void Quando_TempoPreparoForValido_Espero_PropriedadeAtualizada()
            {
                int tempoPreparo = 10;

                sut.SetTempoPreparo(tempoPreparo);

                Assert.Equal(tempoPreparo, sut.TempoPreparo);
            }
        }

        public class SetStatus : ProdutosTestes
        {
            [Fact]
            public void Quando_StatusForValido_Espero_PropriedadeAtualizada()
            {
                var statusProduto = StatusEnum.Ativo;

                sut.SetStatus(statusProduto);

                Assert.Equal(statusProduto, sut.Status);
            }
        }

        public class SetDataCadastro : ProdutosTestes
        {
            [Fact]
            public void Quando_DataCadastroForValido_Espero_PropriedadeAtualizada()
            {
                var dataCadastro = DateTime.Now.Date;

                sut.SetDataCadastro(dataCadastro);

                Assert.Equal(dataCadastro, sut.DataCadastro);
            }

            [Fact]
            public void Quando_DataCadastroAnteriorDataAtual_Espero_Exception()
            {
                var dataCadastro = new DateTime(2026, 10, 26);

                var exception = Assert.Throws<Exception>(() => sut.SetDataCadastro(dataCadastro));

                Assert.Equal("A data de cadastro não pode ser anterior a data atual!", exception.Message);
            }
        }

        public class SetDataAlteracao : ProdutosTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValido_Espero_PropriedadeAtualizada()
            {
                var dataAlteracao = DateTime.Now.Date;

                sut.SetDataAlteracao(dataAlteracao);

                Assert.Equal(dataAlteracao, sut.DataAlteracao);
            }
        }
    }
}
