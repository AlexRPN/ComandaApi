using Comanda.Dominio.Usuarios.Comandos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Usuarios.Repositorios.Filtros;
using Comanda.Dominio.Usuarios.Repositorios.Interfaces;
using Comanda.Dominio.Usuarios.Servicos;
using Comanda.Dominio.Usuarios.Servicos.Interfaces;
using Comanda.Dominio.Utils.Consultas;
using Comanda.Dominio.Utils.Enumeradores;
using Comanda.Dominio.Utils.Filtros.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;

namespace Comanda.Dominio.Testes.Usuarios.Servicos
{
    public class UsuariosServicoTestes
    {
        private readonly UsuarioServico sut;
        private readonly UsuarioComando usuarioComando;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly Usuario usuarioValido;

        public UsuariosServicoTestes()
        {
            usuarioRepositorio = Substitute.For<IUsuarioRepositorio>();
            usuarioComando = Builder<UsuarioComando>
                .CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.EmpresaId = 1)
                .With(x => x.Nome = "Alex")
                .With(x => x.Cpf = "12345678901")
                .With(x => x.Email = "teste@teste.com")
                .With(x => x.Perfil = PerfilEnum.Administrador)
                .With(x => x.Status = StatusEnum.Ativo)
                .With(x => x.DataCadastro = DateTime.Now)
                .Build();
            sut = new(usuarioRepositorio);
        }

        public class ValidarCpfMetodo : UsuariosServicoTestes
        {
            [Fact]
            public async Task Quando_CpfForValido_Espero_NaoLancarExcecao()
            {
                // Arrange
                var cpf = usuarioComando.Cpf;

                // Mock: CPF não existe no sistema (válido)
                usuarioRepositorio.ValidarCpfAsync(cpf, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Usuario?>(null));

                // Act & Assert - não deve lançar exceção
                var resultado = await sut.ValidarCpfAsync(cpf, CancellationToken.None);
                resultado.Should().BeNull();
            }

            [Fact]
            public async Task Quando_CpfJaExisteNoSistema_Espero_Excecao()
            {
                // Arrange
                var usuarioExistente = usuarioComando.Cpf;

                var usuario = new Usuario(new UsuarioComando
                {
                    Nome = usuarioComando.Nome,
                    EmpresaId = usuarioComando.EmpresaId,
                    Cpf = usuarioExistente,
                    Email = usuarioComando.Email,
                    Perfil = usuarioComando.Perfil,
                    Status = usuarioComando.Status,
                    DataCadastro = usuarioComando.DataCadastro
                });

                usuarioRepositorio.ValidarCpfAsync(usuarioExistente, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Usuario?>(usuario));

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentException>(() => sut.ValidarCpfAsync(usuarioExistente, CancellationToken.None));
            }
        }

        public class InserirAsyncMetodo : UsuariosServicoTestes
        {
            [Fact]
            public async Task Quando_UsuarioForValido_Espero_UsuarioInserido()
            {
                // Arrange
                var comando = new UsuarioInserirComando
                {
                    EmpresaId = usuarioComando.EmpresaId,
                    Nome = usuarioComando.Nome,
                    Cpf = usuarioComando.Cpf,
                    Email = usuarioComando.Email,
                    Perfil = usuarioComando.Perfil
                };
                var usuarioEsperado = new Usuario(new UsuarioComando
                {
                    EmpresaId = comando.EmpresaId,
                    Nome = comando.Nome,
                    Cpf = comando.Cpf,
                    Email = comando.Email,
                    Perfil = comando.Perfil,
                    Status = StatusEnum.Ativo,
                    DataCadastro = DateTime.Now
                });

                // Mock ValidarCpfAsync to return null (CPF doesn't exist - valid)
                usuarioRepositorio.ValidarCpfAsync(comando.Cpf, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Usuario?>(null));

                // Mock InserirAsync to return the expected user
                usuarioRepositorio.InserirAsync(Arg.Any<UsuarioComando>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(usuarioEsperado));

                // Act
                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Nome.Should().Be(comando.Nome);
                resultado.Cpf.Should().Be(comando.Cpf);
                resultado.Email.Should().Be(comando.Email);
                resultado.Perfil.Should().Be(comando.Perfil);
                resultado.Status.Should().Be(StatusEnum.Ativo);
            }
        }

        public class FiltrarAsyncMetodo : UsuariosServicoTestes
        {
            [Fact]
            public async Task Quando_FiltrarUsuarios_Espero_RetornarUsuariosFiltrados()
            {
                // Arrange
                var filtro = new UsuarioListarFiltro
                {
                    Id = 1,
                    Cpf = "12345678901"
                };

                var usuarioEsperado = new Usuario(usuarioComando);
                var usuariosQueryable = new List<Usuario> { usuarioEsperado }.AsQueryable();

                // Mock FiltrarAsync to return the expected users
                usuarioRepositorio.FiltrarAsync(Arg.Any<UsuarioListarFiltro>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(usuariosQueryable));

                // Act
                var resultado = await sut.FiltrarAsync(filtro, CancellationToken.None);

                // Assert
                resultado.Should().NotBeEmpty();
                resultado.First().Cpf.Should().Be(filtro.Cpf);
            }
        }

        public class RecuperarPorIdAsyncMetodo : UsuariosServicoTestes
        {
            [Fact]
            public async Task Quando_UsuarioExistir_Espero_RetornarUsuario()
            {
                // Arrange
                var usuarioId = 1;

                // Criação do usuario esperado com base no comando
                var usuarioEsperado = new Usuario(new UsuarioComando
                {
                    EmpresaId = usuarioComando.EmpresaId,
                    Nome = usuarioComando.Nome,
                    Cpf = usuarioComando.Cpf,
                    Email = usuarioComando.Email,
                    Perfil = usuarioComando.Perfil,
                    Status = usuarioComando.Status,
                    DataCadastro = usuarioComando.DataCadastro
                });

                // Mock RecuperarPorIdAsync que retorna o usuário esperado
                usuarioRepositorio.RecuperarPorIdAsync(usuarioId, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Usuario?>(usuarioEsperado));

                // Act
                var resultado = await sut.RecuperarPorIdAsync(usuarioId, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
                resultado.Nome.Should().Be(usuarioComando.Nome);
                resultado.Cpf.Should().Be(usuarioComando.Cpf);
                resultado.Email.Should().Be(usuarioComando.Email);
                resultado.Perfil.Should().Be(PerfilEnum.Administrador);
                resultado.Status.Should().Be(StatusEnum.Ativo);
            }

            [Fact]
            public async Task Quando_UsuarioNaoExistir_Espero_LancarExcecao()
            {
                // Arrange
                var usuarioId = 999; // ID que não existe
                // Mock RecuperarPorIdAsync to return null (user not found)
                usuarioRepositorio.RecuperarPorIdAsync(usuarioId, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<Usuario?>(null));
                // Act & Assert
                await Assert.ThrowsAsync<ArgumentException>(() => sut.RecuperarPorIdAsync(usuarioId, CancellationToken.None));
            }
        }

        public class EditarAsyncMetodo : UsuariosServicoTestes
        {
            [Fact]
            public async Task Quando_UsuarioExistir_Espero_UsuarioEditado()
            {
                // Arrange
                var comando = new UsuarioEditarComando
                {
                    Id = 1,
                    EmpresaId = usuarioComando.EmpresaId,
                    Nome = "Novo Nome",
                    Cpf = "98765432100",
                    Email = "novoemail@example.com",
                    Perfil = PerfilEnum.Usuario
                };

                var usuarioExistente = new Usuario(new UsuarioComando
                {
                    EmpresaId = usuarioComando.EmpresaId,
                    Nome = usuarioComando.Nome,
                    Cpf = usuarioComando.Cpf,
                    Email = usuarioComando.Email,
                    Perfil = usuarioComando.Perfil,
                    Status = usuarioComando.Status,
                    DataCadastro = usuarioComando.DataCadastro
                });

                usuarioRepositorio
                    .RecuperarAsync(comando.Id, Arg.Any<CancellationToken>())
                    .Returns(usuarioExistente);

                usuarioRepositorio
                    .EditarAsync(Arg.Any<Usuario>(), Arg.Any<CancellationToken>())
                    .Returns(callInfo => callInfo.Arg<Usuario>());

                // Act
                var resultado = await sut.EditarAsync(comando, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();

                resultado.Nome.Should().Be(comando.Nome);
                resultado.Cpf.Should().Be(comando.Cpf);
                resultado.Email.Should().Be(comando.Email);
                resultado.Perfil.Should().Be(comando.Perfil);
                resultado.EmpresaId.Should().Be(comando.EmpresaId);

                await usuarioRepositorio.Received(1).EditarAsync(Arg.Any<Usuario>(), Arg.Any<CancellationToken>());
            }

            [Fact]
            public async Task Quando_UsuarioNaoExistir_Espero_Excecao()
            {
                // Arrange
                var comando = new UsuarioEditarComando
                {
                    Id = 999
                };

                usuarioRepositorio.RecuperarAsync(comando.Id, Arg.Any<CancellationToken>()).Returns((Usuario?)null);

                // Act
                Func<Task> act = async () => await sut.EditarAsync(comando, CancellationToken.None);

                // Assert
                await act.Should().ThrowAsync<Exception>().WithMessage("Usuário não encontrado!");
            }
        }

        public class ListarPaginadoAsyncMetodo : UsuariosServicoTestes
        {
            [Fact]
            public async Task Quando_ListarUsuariosPaginado_Espero_RetornarUsuariosPaginados()
            {
                // Arrange
                var usuarios = new List<Usuario>
                {
                    new Usuario(new UsuarioComando
                    {
                        EmpresaId = usuarioComando.EmpresaId,
                        Nome = usuarioComando.Nome,
                        Cpf = usuarioComando.Cpf,
                        Email = usuarioComando.Email,
                        Perfil = usuarioComando.Perfil,
                        Status = usuarioComando.Status,
                        DataCadastro = usuarioComando.DataCadastro
                    })
                }.AsQueryable();
                var paginacaoEsperada = new PaginacaoConsulta<Usuario>
                {
                    Total = 10,
                    Registros = usuarios
                };
                usuarioRepositorio.ListarPaginadoAsync(usuarios, 10, 1, "Nome", TipoOrdenacaoEnum.Asc, Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult(paginacaoEsperada));
                // Act
                var resultado = await sut.ListarPaginadoAsync(usuarios, 10, 1, "Nome", TipoOrdenacaoEnum.Asc, CancellationToken.None);
                // Assert
                resultado.Should().NotBeNull();
                resultado.Registros.Should().HaveCount(1);
                resultado.Total.Should().Be(10);
            }
        }

        public class AlterarStatusAsyncMetodo : UsuariosServicoTestes
        {
            [Fact]
            public async Task Quando_UsuarioExistir_Espero_StatusAlterado()
            {
                // Arrange
                var novoStatus = StatusEnum.Inativo;
                var idUsuario = usuarioComando.Id;
                var comando = new UsuarioEditarComando
                {
                    EmpresaId = usuarioComando.EmpresaId,
                    Nome = usuarioComando.Nome,
                    Cpf = usuarioComando.Cpf,
                    Email = usuarioComando.Email,
                    Perfil = usuarioComando.Perfil
                };
                var usuarioEsperado = new Usuario(new UsuarioComando
                {
                    EmpresaId = comando.EmpresaId,
                    Nome = comando.Nome,
                    Cpf = comando.Cpf,
                    Email = comando.Email,
                    Perfil = comando.Perfil,
                    Status = StatusEnum.Ativo,
                    DataCadastro = DateTime.Now
                });

                usuarioRepositorio.RecuperarPorIdAsync(idUsuario, Arg.Any<CancellationToken>())
                    .Returns(usuarioEsperado);
                // Act
                await sut.AlterarStatusAsync(idUsuario, novoStatus, CancellationToken.None);
                // Assert
                usuarioEsperado.Status.Should().Be(novoStatus);
                await usuarioRepositorio.Received(1).EditarAsync(usuarioEsperado, Arg.Any<CancellationToken>());
            }

            [Fact]
            public async Task Quando_UsuarioNaoExistir_Espero_Excecao()
            {
                // Arrange
                var idUsuario = 999; // ID que não existe
                var novoStatus = StatusEnum.Inativo;

                usuarioRepositorio.RecuperarPorIdAsync(idUsuario, Arg.Any<CancellationToken>()).Returns((Usuario?)null);
                // Act & Assert
                await Assert.ThrowsAsync<ArgumentException>(() => sut.AlterarStatusAsync(idUsuario, novoStatus, CancellationToken.None));
            }
        }
    }
}
