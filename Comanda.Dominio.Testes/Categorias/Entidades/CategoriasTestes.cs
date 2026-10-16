using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Categorias.Entidades
{
    public class CategoriasTestes
    {
        private readonly Categoria sut;
        public CategoriasTestes()
        {
            sut = Builder<Categoria>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                // Arrange
                var comando = new CategoriaComando
                {
                    EmpresaId = 1,
                    Nome = "Categoria Teste",
                    Descricao = "Descrição da Categoria Teste",
                    DataCadastro = DateTime.UtcNow,
                    DataAlteracao = DateTime.UtcNow,
                    Status = StatusEnum.Ativo
                };
                // Act
                var categoria = new Categoria(comando);
                // Assert
                Assert.Equal(comando.EmpresaId, categoria.EmpresaId);
                Assert.Equal(comando.Nome, categoria.Nome);
                Assert.Equal(comando.DataCadastro, categoria.DataCadastro);
                Assert.Equal(comando.DataAlteracao, categoria.DataAlteracao);
                Assert.Equal(comando.Status, categoria.Status);
            }
        }

        public class SetEmpresaId : CategoriasTestes
        {
            [Fact]
            public void Quando_EmpresaIdForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaEmpresaId = 42;
                // Act
                sut.SetEmpresaId(novaEmpresaId);
                // Assert
                Assert.Equal(novaEmpresaId, sut.EmpresaId);
            }
        }

        public class SetNome : CategoriasTestes
        {
            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoNome = "Novo Nome da Categoria";
                // Act
                sut.SetNome(novoNome);
                // Assert
                Assert.Equal(novoNome, sut.Nome);
            }

            [Fact]
            public void Quando_NomeForNulo_Espero_ArgumentNullException()
            {
                // Arrange
                string nomeInvalido = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(nomeInvalido));
                Assert.Equal("O nome da categoria não pode ser nulo ou vazio!", exception.Message);
            }

            [Fact]
            public void Quando_NomeForVazio_Espero_ArgumentNullException()
            {
                // Arrange
                string nomeInvalido = "";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNome(nomeInvalido));
                Assert.Equal("O nome da categoria não pode ser nulo ou vazio!", exception.Message);
            }
        }

        public class SetDescricao : CategoriasTestes
        {
            [Fact]
            public void Quando_DescricaoForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDescricao = "Nova Descrição da Categoria";
                // Act
                sut.SetDescricao(novaDescricao);
                // Assert
                Assert.Equal(novaDescricao, sut.Descricao);
            }

            [Fact]
            public void Quando_DescricaoForNula_Espero_ArgumentNullException()
            {
                // Arrange
                string descricaoInvalida = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetDescricao(descricaoInvalida));
                Assert.Equal("A descrição não pode ser nulo ou vazio!", exception.Message);
            }

            [Fact]
            public void Quando_DescricaoForVazia_Espero_ArgumentNullException()
            {
                // Arrange
                string descricaoInvalida = "";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetDescricao(descricaoInvalida));
                Assert.Equal("A descrição não pode ser nulo ou vazio!", exception.Message);
            }
        }

        public class SetStatus : CategoriasTestes
        {
            [Fact]
            public void Quando_StatusForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoStatus = StatusEnum.Inativo;
                // Act
                sut.SetStatus(novoStatus);
                // Assert
                Assert.Equal(novoStatus, sut.Status);
            }
        }

        public class SetDataCadastro : CategoriasTestes
        {
            [Fact]
            public void Quando_DataCadastroForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDataCadastro = DateTime.UtcNow.AddDays(1);
                // Act
                sut.SetDataCadastro(novaDataCadastro);
                // Assert
                Assert.Equal(novaDataCadastro, sut.DataCadastro);
            }

            [Fact]
            public void Quando_DataCadastroForMenorQueDataAtual_Espero_ArgumentException()
            {
                // Arrange
                var dataInvalida = DateTime.UtcNow.AddDays(-1);
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetDataCadastro(dataInvalida));
                Assert.Equal("A data de cadastro não pode ser menor que a data atual!", exception.Message);
            }
        }

        public class SetDataAlteracao : CategoriasTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDataAlteracao = DateTime.UtcNow.AddDays(1);
                // Act
                sut.SetDataAlteracao(novaDataAlteracao);
                // Assert
                Assert.Equal(novaDataAlteracao, sut.DataAlteracao);
            }
        }
    }
}
