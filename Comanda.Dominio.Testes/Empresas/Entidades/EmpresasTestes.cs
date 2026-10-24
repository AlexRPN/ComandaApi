
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Empresas.Entidades
{
    public class EmpresasTestes
    {
        private readonly Empresa sut;
        public EmpresasTestes()
        {
            sut = Builder<Empresa>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                // Arrange
                var comando = new EmpresaComando
                {
                    NomeFantasia = "Empresa Teste",
                    RazaoSocial = "Razão Social Teste",
                    Cnpj = "00000000000000",
                    InscricaoEstadual = "123456789",
                    Telefone = "62999887766",
                    Email = "teste@teste.com",
                    Logo = "logo.png",
                    BannerPrincipal = "banner.png",
                    Status = StatusEnum.Ativo,
                    DataCadastro = DateTime.Now,
                };

                // Act
                var empresa = new Empresa(comando);

                // Assert
                Assert.Equal(comando.Id, empresa.Id);
                Assert.Equal(comando.NomeFantasia, empresa.NomeFantasia);
                Assert.Equal(comando.RazaoSocial, empresa.RazaoSocial);
                Assert.Equal(comando.Cnpj, empresa.Cnpj);
                Assert.Equal(comando.InscricaoEstadual, empresa.InscricaoEstadual);
                Assert.Equal(comando.Telefone, empresa.Telefone);
                Assert.Equal(comando.Email, empresa.Email);
                Assert.Equal(comando.Logo, empresa.Logo);
                Assert.Equal(comando.BannerPrincipal, empresa.BannerPrincipal);
                Assert.Equal(comando.Status, empresa.Status);
                Assert.Equal(comando.DataCadastro, empresa.DataCadastro);
            }
        }

        public class SetNomeFantasia : EmpresasTestes
        {
            [Fact]
            public void Quando_NomeFantasiaForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoNomeFantasia = "Novo Nome Fantasia";
                // Act
                sut.SetNomeFantasia(novoNomeFantasia);
                // Assert
                Assert.Equal(novoNomeFantasia, sut.NomeFantasia);
            }

            [Fact]
            public void Quando_NomeFantasiaForNulo_Espero_ArgumentException()
            {
                // Arrange
                string nomeFantasiaInvalido = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNomeFantasia(nomeFantasiaInvalido));
                Assert.Equal("Nome Fantasia não pode ser nulo ou vazio!", exception.Message);
            }

            [Fact]
            public void Quando_NomeFantasiaForVazio_Espero_ArgumentException()
            {
                // Arrange
                string nomeFantasiaInvalido = "";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetNomeFantasia(nomeFantasiaInvalido));
                Assert.Equal("Nome Fantasia não pode ser nulo ou vazio!", exception.Message);
            }
        }

        public class SetRazaoSocial : EmpresasTestes
        {
            [Fact]
            public void Quando_RazaoSocialForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaRazaoSocial = "Nova Razão Social";
                // Act
                sut.SetRazaoSocial(novaRazaoSocial);
                // Assert
                Assert.Equal(novaRazaoSocial, sut.RazaoSocial);
            }

            [Fact]
            public void Quando_RazaoSocialForNulo_Espero_ArgumentException()
            {
                // Arrange
                string razaoSocialInvalida = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetRazaoSocial(razaoSocialInvalida));
                Assert.Equal("Razão Social não pode ser nulo ou vazio!", exception.Message);
            }

            [Fact]
            public void Quando_RazaoSocialForVazio_Espero_ArgumentException()
            {
                // Arrange
                string razaoSocialInvalida = "";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetRazaoSocial(razaoSocialInvalida));
                Assert.Equal("Razão Social não pode ser nulo ou vazio!", exception.Message);
            }
        }

        public class SetCnpj : EmpresasTestes
        {
            [Fact]
            public void Quando_CnpjForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoCnpj = "12345678000195";
                // Act
                sut.SetCnpj(novoCnpj);
                // Assert
                Assert.Equal(novoCnpj, sut.Cnpj);
            }

            [Fact]
            public void Quando_CnpjForNulo_Espero_ArgumentException()
            {
                // Arrange
                string cnpjInvalido = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCnpj(cnpjInvalido));
                Assert.Equal("CNPJ não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_CnpjForInvalido_Espero_ArgumentException()
            {
                // Arrange
                string cnpjInvalido = "123";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetCnpj(cnpjInvalido));
                Assert.Equal("CNPJ deve conter 14 caracteres.", exception.Message);
            }
        }

        public class SetInscricaoEstadual : EmpresasTestes
        {
            [Fact]
            public void Quando_InscricaoEstadualForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaInscricaoEstadual = "123456789";
                // Act
                sut.SetInscricaoEstadual(novaInscricaoEstadual);
                // Assert
                Assert.Equal(novaInscricaoEstadual, sut.InscricaoEstadual);
            }
        }

        public class SetTelefone : EmpresasTestes
        {
            [Fact]
            public void Quando_TelefoneForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoTelefone = "62999887766";
                // Act
                sut.SetTelefone(novoTelefone);
                // Assert
                Assert.Equal(novoTelefone, sut.Telefone);
            }

            [Fact]
            public void Quando_TelefoneForNulo_Espero_ArgumentException()
            {
                // Arrange
                string telefoneInvalido = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetTelefone(telefoneInvalido));
                Assert.Equal("Telefone não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_TelefoneForInvalido_Espero_ArgumentException()
            {
                // Arrange
                string telefoneInvalido = "123";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetTelefone(telefoneInvalido));
                Assert.Equal("Telefone deve conter entre 10 e 11 caracteres.", exception.Message);
            }
        }

        public class SetEmail : EmpresasTestes
        {
            [Fact]
            public void Quando_EmailForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoEmail = "teste@teste.com";
                // Act
                sut.SetEmail(novoEmail);
                // Assert
                Assert.Equal(novoEmail, sut.Email);
            }

            [Fact]
            public void Quando_EmailForNulo_Espero_ArgumentException()
            {
                // Arrange
                string emailInvalido = null;
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetEmail(emailInvalido));
                Assert.Equal("Email não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_EmailForVazio_Espero_ArgumentException()
            {
                // Arrange
                string emailInvalido = "";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetEmail(emailInvalido));
                Assert.Equal("Email não pode ser nulo ou vazio.", exception.Message);
            }

            [Fact]
            public void Quando_EmailForInvalido_Espero_ArgumentException()
            {
                // Arrange
                string emailInvalido = "teste.com";
                // Act & Assert
                var exception = Assert.Throws<ArgumentException>(() => sut.SetEmail(emailInvalido));
                Assert.Equal("Email deve conter '@' e '.'", exception.Message);
            }
        }

        public class SetLogo : EmpresasTestes
        {
            [Fact]
            public void Quando_LogoForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaLogo = "logo.png";
                // Act
                sut.SetLogo(novaLogo);
                // Assert
                Assert.Equal(novaLogo, sut.Logo);
            }
        }

        public class SetBannerPrincipal : EmpresasTestes
        {
            [Fact]
            public void Quando_BannerPrincipalForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoBannerPrincipal = "banner.png";
                // Act
                sut.SetBannerPrincipal(novoBannerPrincipal);
                // Assert
                Assert.Equal(novoBannerPrincipal, sut.BannerPrincipal);
            }
        }

        public class SetDataAlteracao : EmpresasTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novaDataAlteracao = DateTime.Now;
                // Act
                sut.SetDataAlteracao(novaDataAlteracao);
                // Assert
                Assert.Equal(novaDataAlteracao, sut.DataAlteracao);
            }
        }
    }
}
