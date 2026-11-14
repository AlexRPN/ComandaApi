
using Comanda.Dominio.Empresas.Servicos.Interfaces;
using Comanda.Dominio.Mesas.Comandos;
using Comanda.Dominio.Mesas.Entidades;
using Comanda.Dominio.Mesas.Repositorios.Interfaces;
using Comanda.Dominio.Mesas.Servicos;
using Comanda.Dominio.Usuarios.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.Mesas.Servicos
{
    public class MesasServicoTestes
    {
        private readonly MesaServico sut;
        private readonly MesaComando mesaComando;
        private readonly IEmpresaServico empresaServicoMock;
        private readonly IMesaRepositorio mesaRepositorioMock;
        public MesasServicoTestes()
        {
            mesaRepositorioMock = Substitute.For<IMesaRepositorio>();
            empresaServicoMock = Substitute.For<IEmpresaServico>();
            mesaComando = Builder<MesaComando>
                .CreateNew()
                .With(x => x.EmpresaId = 1)
                .With(x => x.Numero = 1)
                .With(x => x.Capacidade = 4)
                .With(x => x.DataCadastro = DateTime.Now.Date)
                .With(x => x.DataAlteracao = DateTime.Now.Date)
                .With(x => x.Situacao = SituacaoMesaEnum.Livre)
                .With(x => x.Status = StatusEnum.Ativo)
                .Build();
            sut = new(mesaRepositorioMock, empresaServicoMock);
        }

        public class InserirAsync : MesasServicoTestes
        {
            [Fact]
            public async Task Quando_MesaForValida_Espero_MesaInserida()
            {
                var comando = mesaComando;

                var mesaInserida = new Mesa(new MesaComando
                {
                    EmpresaId = comando.EmpresaId,
                    Numero = comando.Numero,
                    Capacidade = comando.Capacidade,
                    DataCadastro = comando.DataCadastro,
                    DataAlteracao = comando.DataAlteracao,
                    Situacao = comando.Situacao,
                    Status = comando.Status
                });

                mesaRepositorioMock.InserirAsync(Arg.Any<Mesa>(), Arg.Any<CancellationToken>()).Returns(mesaInserida);

                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.EmpresaId.Should().Be(comando.EmpresaId);
                resultado.Numero.Should().Be(comando.Numero);
                resultado.Capacidade.Should().Be(comando.Capacidade);
                resultado.DataCadastro.Should().Be(comando.DataCadastro);
                resultado.DataAlteracao.Should().Be(comando.DataAlteracao);
                resultado.Situacao.Should().Be(comando.Situacao);
                resultado.Status.Should().Be(comando.Status);
            }

            [Fact]
            public async Task Quando_MesaJaExistente_Espero_ArgumentExceptionAsync()
            {
                var mesaInserida = new MesaComando
                {
                    EmpresaId = mesaComando.EmpresaId,
                    Numero = mesaComando.Numero,
                    Capacidade = mesaComando.Capacidade,
                    DataCadastro = mesaComando.DataCadastro,
                    DataAlteracao = mesaComando.DataAlteracao,
                    Situacao = mesaComando.Situacao,
                    Status = mesaComando.Status
                };

                var mesaExistente = new Mesa(mesaComando);

                mesaRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<Mesa, bool>>>(),
                                                   Arg.Any<CancellationToken>())
                                   .Returns(Task.FromResult<Mesa?>(mesaExistente));

                Func<Task> act = async () => await sut.InserirAsync(mesaInserida, CancellationToken.None);

                await act.Should()
                         .ThrowAsync<ArgumentException>()
                         .WithMessage("Já existe uma mesa com o mesmo número para esta empresa.");
            }

            [Fact]
            public async Task Quando_MesaNaoForInserido_Espero_ArgumentExceptionAsync()
            {
                var mesaInserida = new MesaComando
                {
                    EmpresaId = mesaComando.EmpresaId,
                    Numero = mesaComando.Numero,
                    Capacidade = mesaComando.Capacidade,
                    DataCadastro = mesaComando.DataCadastro,
                    DataAlteracao = mesaComando.DataAlteracao,
                    Situacao = mesaComando.Situacao,
                    Status = mesaComando.Status
                };

                var mesaExistente = new Mesa(mesaComando);

                mesaRepositorioMock.InserirAsync(Arg.Any<Mesa>(), Arg.Any<CancellationToken>())
                                   .Returns(Task.FromResult<Mesa?>(null));

                Func<Task> act = async () => await sut.InserirAsync(mesaInserida, CancellationToken.None);

                await act.Should()
                         .ThrowAsync<ArgumentException>()
                         .WithMessage("Não foi possível inserir a mesa.");
            }
        }
    }
}
