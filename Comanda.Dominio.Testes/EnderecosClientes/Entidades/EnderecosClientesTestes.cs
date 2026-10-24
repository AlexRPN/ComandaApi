
using Comanda.Dominio.EnderecoClientes.Comandos;
using Comanda.Dominio.EnderecoClientes.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.EnderecosClientes.Entidades
{
    public class EnderecosClientesTestes
    {
        private readonly EnderecoCliente sut;
        public EnderecosClientesTestes()
        {
            sut = Builder<EnderecoCliente>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                // Arrange
                var comando = new EnderecoClienteComando
                {
                    ClienteId = 1,
                    Cep = "74937020",
                    Logradouro = "Logradouro teste",
                    Numero = "33",
                    Complemento = "Complemento teste",
                    Bairro = "Bairro teste",
                    Cidade = "Cidade teste",
                    Estado = "Estado teste",
                    PontoReferencia = "Supermercado tatico"
                };

                // Act
                var enderecoCliente = new EnderecoCliente(comando);

                // Assert
                Assert.Equal(comando.ClienteId, enderecoCliente.ClienteId);
                Assert.Equal(comando.Cep, enderecoCliente.Cep);
                Assert.Equal(comando.Logradouro, enderecoCliente.Logradouro);
                Assert.Equal(comando.Numero, enderecoCliente.Numero);
                Assert.Equal(comando.Complemento, enderecoCliente.Complemento);
                Assert.Equal(comando.Bairro, enderecoCliente.Bairro);
                Assert.Equal(comando.Cidade, enderecoCliente.Cidade);
                Assert.Equal(comando.Estado, enderecoCliente.Estado);
            }
        }

        public class SetClienteId : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_ClienteIdForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var idCliente = 1;

                // Act
                sut.SetClienteId(idCliente);

                // Assert
                Assert.Equal(idCliente, sut.ClienteId);
            }
        }

        public class SetCep : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_CepForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var cep = "74937020";

                // Act
                sut.SetCep(cep);

                // Assert
                Assert.Equal(cep, sut.Cep);
            }

            [Fact]
            public void Quando_CepForNulo_Espero_Excecao()
            {
                // Arrange
                string novoCep = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCep(novoCep));
                Assert.Equal("O CEP do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_CepForVazio_Espero_Excecao()
            {
                // Arrange
                string novoCep = "";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCep(novoCep));
                Assert.Equal("O CEP do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_CepPossuirMaisDeNoveCaracteres_Espero_Excecao()
            {
                // Arrange
                string novoCep = "1234567890";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCep(novoCep));
                Assert.Equal("O CEP deve conter entre 2 e 9 caracteres.", exception.Message);
            }

            [Fact]
            public void Quando_CepPossuirMenosDeDoisCaracteres_Espero_Excecao()
            {
                // Arrange
                string novoCep = "1";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCep(novoCep));
                Assert.Equal("O CEP deve conter entre 2 e 9 caracteres.", exception.Message);
            }
        }

        public class SetLogradouro : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_LogradouroForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoLogradouro = "Avenida Teste";

                // Act
                sut.SetLogradouro(novoLogradouro);

                // Assert
                Assert.Equal(novoLogradouro, sut.Logradouro);
            }

            [Fact]
            public void Quando_LogradouroForNulo_Espero_ArgumentException()
            {
                // Arrange
                string logradouro = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetLogradouro(logradouro));
                Assert.Equal("O logradouro do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_LogradouroForVazio_Espero_ArgumentException()
            {
                // Arrange
                string logradouro = "";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetLogradouro(logradouro));
                Assert.Equal("O logradouro do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }
        }

        public class SetNumero : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_NumeroForValido_Espero_PropriedadeAtualizada()
            {
                var numero = "1";

                sut.SetNumero(numero);

                Assert.Equal(numero, sut.Numero);
            }
        }

        public class SetComplemento : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_ComplementoForValido_Espero_PropriedadeAtualizada()
            {
                var complemento = "Complemento teste";

                sut.SetComplemento(complemento);

                Assert.Equal(complemento, sut.Complemento);
            }
        }

        public class SetBairro : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_BairroForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var bairro = "Bairro Teste";

                // Act
                sut.SetBairro(bairro);

                // Assert
                Assert.Equal(bairro, sut.Bairro);
            }

            [Fact]
            public void Quando_BairroForNulo_Espero_ArgumentException()
            {
                //Arrange
                string bairro = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetBairro(bairro));
                Assert.Equal("O bairro do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_BairroForVazio_Espero_ArgumentException()
            {
                //Arrange
                var bairro = "";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetBairro(bairro));
                Assert.Equal("O bairro do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }
        }

        public class SetCidade : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_CidadeForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var cidade = "Cidade Teste";

                // Act
                sut.SetCidade(cidade);

                // Assert
                Assert.Equal(cidade, sut.Cidade);
            }

            [Fact]
            public void Quando_CidadeForNulo_Espero_ArgumentException()
            {
                //Arrange
                string cidade = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCidade(cidade));
                Assert.Equal("A cidade do endereço do cliente não pode ser nula ou vazia.", exception.Message);
            }

            [Fact]
            public void Quando_CidadeForVazio_Espero_ArgumentException()
            {
                //Arrange
                string cidade = "";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCidade(cidade));
                Assert.Equal("A cidade do endereço do cliente não pode ser nula ou vazia.", exception.Message);
            }
        }

        public class SetEstado : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_EstadoForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var estado = "Estado Teste";

                // Act
                sut.SetEstado(estado);

                // Assert
                Assert.Equal(estado, sut.Estado);
            }

            [Fact]
            public void Quando_EstadoForNulo_Espero_ArgumentException()
            {
                //Arrange
                string estado = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetEstado(estado));
                Assert.Equal("O estado do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_EstadoForVazio_Espero_ArgumentException()
            {
                //Arrange
                string estado = "";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetEstado(estado));
                Assert.Equal("O estado do endereço do cliente não pode ser nulo ou vazio.", exception.Message);
            }
        }

        public class SetPontoReferencia : EnderecosClientesTestes
        {
            [Fact]
            public void Quando_PontoReferenciaForValido_Espero_PropriedadeAtualizada()
            {
                var pontoReferencia = "Ponto referência teste";

                sut.SetPontoReferencia(pontoReferencia);

                Assert.Equal(pontoReferencia, sut.PontoReferencia);
            }
        }
    }
}
