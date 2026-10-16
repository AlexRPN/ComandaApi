using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.Adicionais.Repositorios.Interfaces;
using Comanda.Dominio.Adicionais.Servicos;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;

namespace Comanda.Dominio.Testes.Adicionais.Servicos
{
    public class AdicionaisServicoTestes
    {
        private readonly AdicionalServico sut;
        private readonly AdicionalComando adicionalComando;
        private readonly IAdicionalRepositorio adicionalRepositorioMock;
        public AdicionaisServicoTestes()
        {
            adicionalRepositorioMock = Substitute.For<IAdicionalRepositorio>();
            sut = new AdicionalServico(adicionalRepositorioMock);
            adicionalComando = Builder<AdicionalComando>.CreateNew()
                .With(x => x.GrupoAdicionalId = 1)
                .With(x => x.Nome = "Adicional Teste")
                .With(x => x.Valor = 10.0m)
                .With(x => x.Status = StatusEnum.Ativo)
                .With(x => x.DataCadastro = DateTime.UtcNow)
                .With(x => x.DataAlteracao = DateTime.UtcNow)
                .Build();
        }

        [Fact]
        public async Task Quando_InserirAsyncForChamado_Espero_AdicionarAdicionais()
        {
            // Arrange
            var comando = new List<AdicionalComando>
            {
                new AdicionalComando
                {
                    GrupoAdicionalId = 1,
                    Nome = "Adicional Teste 1",
                    Valor = 10.0m,
                    Status = StatusEnum.Ativo,
                    DataCadastro = DateTime.UtcNow,
                    DataAlteracao = DateTime.UtcNow
                },
                new AdicionalComando
                {
                    GrupoAdicionalId = 2,
                    Nome = "Adicional Teste 2",
                    Valor = 20.0m,
                    Status = StatusEnum.Ativo,
                    DataCadastro = DateTime.UtcNow,
                    DataAlteracao = DateTime.UtcNow
                }
            };

            // Lista de adicionais que será retornada pelo mock do repositório
            var adicionaisRetorno = comando.Select(x => new Adicional(x)).ToList();

            // Mock do repositório para retornar os adicionais inseridos
            adicionalRepositorioMock.InserirAsync(Arg.Any<List<AdicionalComando>>(), Arg.Any<CancellationToken>())
                .Returns(adicionaisRetorno);

            // Act
            var resultado = await sut.InserirAsync(comando, CancellationToken.None);

            // Assert
            resultado.Should().NotBeNull();

            // Verifica se o método InserirAsync do repositório foi chamado com os parâmetros corretos
            await adicionalRepositorioMock.Received(1).InserirAsync(Arg.Any<List<AdicionalComando>>(), Arg.Any<CancellationToken>());
        }
    }
}
