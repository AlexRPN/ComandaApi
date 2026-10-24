
using Comanda.Dominio.EnderecosEmpresas.Comandos;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.EnderecosEmpresas.Entidades
{
    public class EnderecosEmpresasTestes
    {
        private readonly EnderecoEmpresa sut;
        public EnderecosEmpresasTestes()
        {
            sut = Builder<EnderecoEmpresa>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                // Arrange
                var comando = new EnderecoEmpresaComando
                {
                    EmpresaId = 1,
                    Logradouro = "Rua Teste",
                    Numero = "123",
                    Complemento = "Apto 1",
                    Bairro = "Bairro Teste",
                    Cidade = "Cidade Teste",
                    Estado = "Estado Teste",
                    Pais = "Pais Teste",
                    Cep = "12345678",
                    Latitude = 12.345678m,
                    Longitude = 98.765432m,
                };

                // Act
                var enderecoEmpresa = new EnderecoEmpresa(comando);

                // Assert
                Assert.Equal(comando.EmpresaId, enderecoEmpresa.EmpresaId);
                Assert.Equal(comando.Logradouro, enderecoEmpresa.Logradouro);
                Assert.Equal(comando.Numero, enderecoEmpresa.Numero);
                Assert.Equal(comando.Complemento, enderecoEmpresa.Complemento);
                Assert.Equal(comando.Bairro, enderecoEmpresa.Bairro);
                Assert.Equal(comando.Cidade, enderecoEmpresa.Cidade);
                Assert.Equal(comando.Estado, enderecoEmpresa.Estado);
                Assert.Equal(comando.Pais, enderecoEmpresa.Pais);
                Assert.Equal(comando.Cep, enderecoEmpresa.Cep);
                Assert.Equal(comando.Latitude, enderecoEmpresa.Latitude);
                Assert.Equal(comando.Longitude, enderecoEmpresa.Longitude);
            }
        }

        public class SetEmpresaId : EnderecosEmpresasTestes
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

        public class SetCep : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_CepForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoCep = "87654321";

                // Act
                sut.SetCep(novoCep);

                // Assert
                Assert.Equal(novoCep, sut.Cep);
            }

            [Fact]
            public void Quando_CepForNulo_Espero_Excecao()
            {
                // Arrange
                string novoCep = null;

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCep(novoCep));
                Assert.Equal("O CEP não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_CepForVazio_Espero_Excecao()
            {
                // Arrange
                string novoCep = "";

                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCep(novoCep));
                Assert.Equal("O CEP não pode ser nulo ou vazio.", exception.Message);
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

        public class SetLogradouro : EnderecosEmpresasTestes
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
        }

        public class SetNumero : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_NumeroForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoNumero = "456";

                // Act
                sut.SetNumero(novoNumero);

                // Assert
                Assert.Equal(novoNumero, sut.Numero);
            }
        }

        public class SetComplemento : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_ComplementoForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var complemento = "Complemento teste";

                // Act
                sut.SetComplemento(complemento);

                // Assert
                Assert.Equal(complemento, sut.Complemento);
            }
        }

        public class SetBairro : EnderecosEmpresasTestes
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
        }

        public class SetCidade : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_CidadeForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var cidade = sut.Cidade;

                // Act
                sut.SetCidade(cidade);

                // Assert
                Assert.Equal(cidade, sut.Cidade);
            }
        }

        public class SetEstado : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_EstadoForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var estado = sut.Estado;

                // Act
                sut.SetEstado(estado);

                // Assert
                Assert.Equal(estado, sut.Estado);
            }
        }

        public class SetPais : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_PaisForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var pais = sut.Pais;

                // Act
                sut.SetPais(pais);

                // Assert
                Assert.Equal(pais, sut.Pais);
            }
        }

        public class SetLatitude : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_LatitudeForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var latitude = sut.Latitude;

                // Act
                sut.SetLatitude(latitude);

                // Assert
                Assert.Equal(latitude, sut.Latitude);
            }
        }

        public class SetLongitude : EnderecosEmpresasTestes
        {
            [Fact]
            public void Quando_LongitudeForValido_Espero_PropriedadeAtualizada()
            {
                //Arrange
                var longitude = sut.Longitude;

                // Act
                sut.SetLongitude(longitude);

                // Assert
                Assert.Equal(longitude, sut.Longitude);
            }
        }
    }
}
