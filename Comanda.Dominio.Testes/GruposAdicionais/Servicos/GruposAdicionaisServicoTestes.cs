
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.GrupoAdicionais.Repositorios.Interfaces;
using Comanda.Dominio.GrupoAdicionais.Servicos;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.GruposAdicionais.Servicos
{
    public class GruposAdicionaisServicoTestes
    {
        private readonly GrupoAdicionalServico sut;
        //private readonly GrupoAdicionalComando grupoAdicionalComando;
        private readonly IGrupoAdicionalRepositorio grupoAdicionalRepositorioMock;
        public GruposAdicionaisServicoTestes()
        {
            grupoAdicionalRepositorioMock = Substitute.For<IGrupoAdicionalRepositorio>();
            sut = new GrupoAdicionalServico(grupoAdicionalRepositorioMock);
        }

        public class InserirAsyncMetodo : GruposAdicionaisServicoTestes
        {
            [Fact]
            public async Task Quando_InserirAsyncForChamado_Espero_GrupoAdicionalCriado()
            {
                // Arrange
                var comando = new List<GrupoAdicionalComando>
                {
                    new GrupoAdicionalComando
                    {
                        EmpresaId = 1,
                        Nome = "Adicional teste 1"
                    },
                    new GrupoAdicionalComando
                    {
                        EmpresaId = 1,
                        Nome = "Adicional teste 2"
                    }

                };

                // Lista de grupos adicionais que será retornada pelo mock do repositório
                var gruposAdicionaisRetorno = comando.Select(x => new GrupoAdicional(x)).ToList();

                // Mock do repositório para retornar os grupos adicionais inseridos
                grupoAdicionalRepositorioMock.InserirAsync(Arg.Any<List<GrupoAdicional>>(), Arg.Any<CancellationToken>()).Returns(gruposAdicionaisRetorno);

                // Act
                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                // Assert
                resultado.Should().NotBeNull();
            }
        }

        public class RecuperarAsyncMetodo : GruposAdicionaisServicoTestes
        {
            [Fact]
            public async Task Quando_GrupoAdicionalValido_Espero_GrupoAdicionalRecuperado()
            {
                var empresaId = 1;
                var grupoAdicionalId = 1;

                var grupoAdicionalEsperado = new GrupoAdicional(new GrupoAdicionalComando
                {
                    Id = 1,
                    EmpresaId = 1,
                    Nome = "Grupo adicional teste"
                });

                grupoAdicionalRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<GrupoAdicional, bool>>>(), CancellationToken.None)
                    .Returns(grupoAdicionalEsperado);

                var resultado = sut.RecuperarAsync(empresaId, grupoAdicionalId, CancellationToken.None);

                Assert.NotNull(resultado);
            }

            [Fact]
            public async Task Quando_GrupoAdicionalForNulo_Espero_Exception()
            {
                // Arrange
                var empresaId = 1;
                var grupoAdicionalId = 1;

                grupoAdicionalRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<GrupoAdicional, bool>>>(),
                                                             Arg.Any<CancellationToken>())
                                             .Returns((GrupoAdicional?)null);

                // Act
                Func<Task> act = () => sut.RecuperarAsync(empresaId, grupoAdicionalId, CancellationToken.None);

                // Assert
                await act.Should().ThrowAsync<Exception>()
                                  .WithMessage($"Grupo adicional {grupoAdicionalId} não encontrado para esta empresa!");

                await grupoAdicionalRepositorioMock.Received(1).RecuperarAsync(Arg.Any<Expression<Func<GrupoAdicional, bool>>>(),
                                                                               Arg.Any<CancellationToken>());
            }
        }
    }
}
