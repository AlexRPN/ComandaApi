
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Repositorios.Interfaces;
using Comanda.Dominio.HorariosFuncionamento.Servicos;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace Comanda.Dominio.Testes.HorariosFuncionamentos.Servicos
{
    public class HorariosFuncionamentosServicoTestes
    {
        private readonly HorarioFuncionamentoServico sut;
        private readonly HorarioFuncionamentoComando horarioFuncionamentoComando;
        private readonly IHorarioFuncionamentoRepositorio horarioFuncionamentoRepositorioMock;
        public HorariosFuncionamentosServicoTestes()
        {
            horarioFuncionamentoComando = Builder<HorarioFuncionamentoComando>
                .CreateNew()
                .With(x => x.Id = 1)
                .With(x => x.EmpresaId = 1)
                .With(x => x.DiaSemana = DiaSemanaEnum.Domingo)
                .With(x => x.HoraAbertura = DateTime.Now.AddHours(18))
                .With(x => x.HoraFechamento = DateTime.Now.AddHours(23))
                .With(x => x.Status = StatusEnum.Ativo)
                .Build();
            horarioFuncionamentoRepositorioMock = Substitute.For<IHorarioFuncionamentoRepositorio>();
            sut = new HorarioFuncionamentoServico(horarioFuncionamentoRepositorioMock);
        }

        public class InserirAsyncMetodo : HorariosFuncionamentosServicoTestes
        {
            [Fact]
            public async Task Quando_InserirAsyncForChamado_Espero_HorarioFuncionamentoCriado()
            {
                var horarioInserido = new List<HorarioFuncionamento>();

                var comando = new List<HorarioFuncionamentoComando>
                {
                    new HorarioFuncionamentoComando
                    {
                        EmpresaId = horarioFuncionamentoComando.EmpresaId,
                        DiaSemana = horarioFuncionamentoComando.DiaSemana,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento,
                        Status = horarioFuncionamentoComando.Status
                    }
                };

                horarioInserido = comando.Select(x => new HorarioFuncionamento(x)).ToList();

                horarioFuncionamentoRepositorioMock.InserirAsync(Arg.Any<List<HorarioFuncionamentoComando>>(), Arg.Any<CancellationToken>()).Returns(horarioInserido);

                var resultado = await sut.InserirAsync(comando, CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.ToList().ForEach(x => x.EmpresaId.Should().Be(horarioFuncionamentoComando.EmpresaId));
                resultado.ToList().ForEach(x => x.DiaSemana.Should().Be(horarioFuncionamentoComando.DiaSemana));
                resultado.ToList().ForEach(x => x.HoraAbertura.Should().Be(horarioFuncionamentoComando.HoraAbertura));
                resultado.ToList().ForEach(x => x.HoraFechamento.Should().Be(horarioFuncionamentoComando.HoraFechamento));
                resultado.ToList().ForEach(x => x.Status.Should().Be(horarioFuncionamentoComando.Status));
            }
        }

        public class RecuperarAsyncMetodo : HorariosFuncionamentosServicoTestes
        {
            [Fact]
            public async Task Quando_EmpresaForValida_Espero_RetornarHorariosFuncionamento()
            {
                var horariosFuncionamento = new List<HorarioFuncionamento>
                {
                    new HorarioFuncionamento(horarioFuncionamentoComando)
                };

                horarioFuncionamentoRepositorioMock.ListarAsync(Arg.Any<Expression<Func<HorarioFuncionamento, bool>>>(), Arg.Any<CancellationToken>()).Returns(horariosFuncionamento);

                var resultado = await sut.RecuperarAsync(horarioFuncionamentoComando.EmpresaId, CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.ToList().ForEach(x => x.EmpresaId.Should().Be(horarioFuncionamentoComando.EmpresaId));
                resultado.ToList().ForEach(x => x.DiaSemana.Should().Be(horarioFuncionamentoComando.DiaSemana));
                resultado.ToList().ForEach(x => x.HoraAbertura.Should().Be(horarioFuncionamentoComando.HoraAbertura));
                resultado.ToList().ForEach(x => x.HoraFechamento.Should().Be(horarioFuncionamentoComando.HoraFechamento));
                resultado.ToList().ForEach(x => x.Status.Should().Be(horarioFuncionamentoComando.Status));
            }
        }

        public class EditarAsyncMetodo : HorariosFuncionamentosServicoTestes
        {
            [Fact]
            public async Task Quando_EditarAsyncForChamado_Espero_HorarioFuncionamentoEditado()
            {
                var comandoEditar = new List<HorarioFuncionamentoEditarComando>
                {
                    new HorarioFuncionamentoEditarComando
                    {
                        DiaSemana = horarioFuncionamentoComando.DiaSemana,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura.AddHours(1),
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento.AddHours(1)
                    }
                };

                var horariosExistentes = new List<HorarioFuncionamento>
                {
                    new HorarioFuncionamento(horarioFuncionamentoComando)
                };

                horarioFuncionamentoRepositorioMock.ListarAsync(Arg.Any<Expression<Func<HorarioFuncionamento, bool>>>(), Arg.Any<CancellationToken>()).Returns(horariosExistentes);

                var resultado = await sut.EditarAsync(horarioFuncionamentoComando.EmpresaId, comandoEditar, CancellationToken.None);

                resultado.Should().NotBeNull();
                resultado.ToList().ForEach(x => x.EmpresaId.Should().Be(horarioFuncionamentoComando.EmpresaId));
                resultado.ToList().ForEach(x => x.DiaSemana.Should().Be(horarioFuncionamentoComando.DiaSemana));
                resultado.ToList().ForEach(x => x.HoraAbertura.Should().Be(comandoEditar.First().HoraAbertura));
                resultado.ToList().ForEach(x => x.HoraFechamento.Should().Be(comandoEditar.First().HoraFechamento));
            }

            [Fact]
            public async Task Quando_EditarAsyncForChamadoSemHorarios_Espero_ArgumentException()
            {
                var comandoEditar = new List<HorarioFuncionamentoEditarComando>();

                Func<Task> act = async () => await sut.EditarAsync(horarioFuncionamentoComando.EmpresaId, comandoEditar, CancellationToken.None);

                await act.Should().ThrowAsync<ArgumentException>().WithMessage("Nenhum horário informado.");
            }
        }
    }
}
