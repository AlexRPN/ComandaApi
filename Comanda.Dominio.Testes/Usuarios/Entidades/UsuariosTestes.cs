using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FluentAssertions;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Usuarios.Entidades
{
    public class UsuariosTestes
    {
        private readonly Usuario sut;
        public UsuariosTestes()
        {
            sut = Builder<Usuario>.CreateNew().Build();
        }

        public class SetEmpresaId : UsuariosTestes
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

        public class SetPerfil : UsuariosTestes
        {
            [Fact]
            public void Quando_PerfilForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var perfil = PerfilEnum.Usuario;
                // Act
                sut.SetPerfil(perfil);
                // Assert
                Assert.Equal(perfil, sut.Perfil);
            }
        }

        public class SetStatus : UsuariosTestes
        {
            [Fact]
            public void Quando_StatusForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var status = StatusEnum.Inativo;
                // Act
                sut.SetStatus(status);
                // Assert
                Assert.Equal(status, sut.Status);
            }
        }

        public class SetDataCadastro : UsuariosTestes
        {
            [Fact]
            public void Quando_DataCadastroForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var data = new DateTime(2020, 1, 1);
                // Act
                sut.SetDataCadastro(data);
                // Assert
                Assert.Equal(data, sut.DataCadastro);
            }
        }

        public class SetDataAlteracao : UsuariosTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValida_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var data = DateTime.UtcNow;
                // Act
                sut.SetDataAlteracao(data);
                // Assert
                Assert.Equal(data, sut.DataAlteracao);
            }
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ParametrosForemValidos_Espero_ObjetoIntegro()
            {
                string nome = "Alex";
                int empresaId = 1;
                string cpf = "12345678900";
                string email = "usuario@teste.com";
                PerfilEnum perfil = PerfilEnum.Administrador;
                StatusEnum status = StatusEnum.Ativo;
                DateTime dataCadastro = DateTime.Now;

                Usuario usuario = new Usuario(new UsuarioComando
                {
                    Nome = nome,
                    EmpresaId = empresaId,
                    Cpf = cpf,
                    Email = email,
                    Perfil = perfil,
                    Status = status,
                    DataCadastro = dataCadastro
                });

                usuario.Nome.Should().Be(nome);
                usuario.EmpresaId.Should().Be(empresaId);
                usuario.Cpf.Should().Be(cpf);
                usuario.Email.Should().Be(email);
                usuario.Perfil.Should().Be(perfil);
                usuario.Status.Should().Be(status);
                usuario.DataCadastro.Should().Be(dataCadastro);
            }
        }

        public class SetNome : UsuariosTestes
        {
            [Fact]
            public void Quando_NomeForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoNome = "Novo Nome";
                // Act
                sut.SetNome(novoNome);
                // Assert
                Assert.Equal(novoNome, sut.Nome);
            }

            [Fact]
            public void Quando_NomeForInvalido_Espero_Excecao()
            {
                // Arrange
                var nomeInvalido = "";
                // Act & Assert
                Assert.Throws<ArgumentException>(() => sut.SetNome(nomeInvalido));
            }
        }

        public class SetCpf : UsuariosTestes
        {
            [Fact]
            public void Quando_CpfForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoCpf = "12345678900";
                // Act
                sut.SetCpf(novoCpf);
                // Assert
                Assert.Equal(novoCpf, sut.Cpf);
            }
            [Fact]
            public void Quando_CpfForInvalido_Espero_Excecao()
            {
                // Arrange
                var cpfInvalido = "";
                // Act & Assert
                Assert.Throws<ArgumentException>(() => sut.SetCpf(cpfInvalido));
            }
        }

        public class SetEmail : UsuariosTestes
        {
            [Fact]
            public void Quando_EmailForValido_Espero_PropriedadeAtualizada()
            {
                // Arrange
                var novoEmail = "novoemail@teste.com";
                // Act
                sut.SetEmail(novoEmail);
                // Assert
                Assert.Equal(novoEmail, sut.Email);
            }

            [Fact]
            public void Quando_EmailForInvalido_Espero_Excecao()
            {
                // Arrange
                var emailInvalido = "emailinvalido";
                // Act & Assert
                Assert.Throws<ArgumentException>(() => sut.SetEmail(emailInvalido));
            }
        }
    }
}
