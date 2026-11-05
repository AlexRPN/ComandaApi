
using Comanda.Dominio.Empresas.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Empresas.Servicos.Interfaces;
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
        private readonly IEmpresaServico empresaServicoMock;
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
            empresaServicoMock = Substitute.For<IEmpresaServico>();
            empresaServicoMock.RecuperarAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                .Returns(new Empresa(new EmpresaComando
                {
                    NomeFantasia = "Empresa Teste",
                    RazaoSocial = "Empresa Teste LTDA",
                    Cnpj = "12345678000199",
                    Telefone = "62999887766",
                    Email = "empresa@teste.com"
                }));
            sut = new HorarioFuncionamentoServico(horarioFuncionamentoRepositorioMock, empresaServicoMock);
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

            [Fact]
            public void Quando_EmpresaForNulo_Espero_ArgumentException()
            {
                var comando = new List<HorarioFuncionamentoComando>
                {
                    new HorarioFuncionamentoComando
                    {
                        EmpresaId = 999,
                        DiaSemana = horarioFuncionamentoComando.DiaSemana,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento,
                        Status = horarioFuncionamentoComando.Status
                    }
                };
                empresaServicoMock.RecuperarAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
                    .Returns((Empresa)null);

                Func<Task> act = async () => await sut.InserirAsync(comando, CancellationToken.None);

                act.Should().ThrowAsync<ArgumentException>().WithMessage("Empresa não encontrada!");
            }

            [Fact]
            public async Task Quando_EmpresaForDiferente_Espero_ArgumentException()
            {
                var comando = new List<HorarioFuncionamentoComando>
                {
                    new HorarioFuncionamentoComando
                    {
                        EmpresaId = 1,
                        DiaSemana = horarioFuncionamentoComando.DiaSemana,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento,
                        Status = horarioFuncionamentoComando.Status
                    },
                    new HorarioFuncionamentoComando
                    {
                        EmpresaId = 2,
                        DiaSemana = DiaSemanaEnum.Segunda,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento,
                        Status = horarioFuncionamentoComando.Status
                    }
                };

                Func<Task> act = () =>
                    sut.InserirAsync(comando, CancellationToken.None);

                await act.Should()
                    .ThrowAsync<ArgumentException>()
                    .WithMessage("Todos os horários devem pertencer à mesma empresa!");
            }

            [Fact]
            public void Quando_HorarioDuplicadoForInserido_Espero_ArgumentException()
            {
                var comando = new List<HorarioFuncionamentoComando>
                {
                    new HorarioFuncionamentoComando
                    {
                        EmpresaId = horarioFuncionamentoComando.EmpresaId,
                        DiaSemana = horarioFuncionamentoComando.DiaSemana,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento,
                        Status = horarioFuncionamentoComando.Status
                    },
                    new HorarioFuncionamentoComando
                    {
                        EmpresaId = horarioFuncionamentoComando.EmpresaId,
                        DiaSemana = horarioFuncionamentoComando.DiaSemana,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura.AddHours(1),
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento.AddHours(1),
                        Status = horarioFuncionamentoComando.Status
                    }
                };

                Func<Task> act = async () => await sut.InserirAsync(comando, CancellationToken.None);

                act.Should().ThrowAsync<ArgumentException>().WithMessage("Não é permitido inserir horários de funcionamento duplicados para o mesmo dia da semana!");
            }

            [Fact]
            public void Quando_HorarioExistenteForInserido_Espero_ArgumentException()
            {
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

                var horarioExistente = new HorarioFuncionamento(horarioFuncionamentoComando);

                horarioFuncionamentoRepositorioMock.RecuperarAsync(Arg.Any<Expression<Func<HorarioFuncionamento, bool>>>(), 
                                                                   Arg.Any<CancellationToken>())
                                                   .Returns(horarioExistente);

                Func<Task> act = async () => await sut.InserirAsync(comando, CancellationToken.None);

                act.Should().ThrowAsync<ArgumentException>().WithMessage($"Já existe horário cadastrado para {horarioFuncionamentoComando.DiaSemana}.");
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
                        EmpresaId = horarioFuncionamentoComando.EmpresaId,
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

                var resultado = await sut.EditarAsync(comandoEditar, CancellationToken.None);

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

                Func<Task> act = async () => await sut.EditarAsync(comandoEditar, CancellationToken.None);

                await act.Should().ThrowAsync<ArgumentException>().WithMessage("Nenhum horário informado.");
            }

            [Fact]
            public async Task Quando_EditarAsyncForChamadoComHorarioInexistente_Espero_HorarioFuncionamentoCriado()
            {
                var comandoEditar = new List<HorarioFuncionamentoEditarComando>
                {
                    new HorarioFuncionamentoEditarComando
                    {
                        EmpresaId = horarioFuncionamentoComando.EmpresaId,
                        DiaSemana = DiaSemanaEnum.Segunda,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento
                    }
                };

                var horariosExistentes = new List<HorarioFuncionamento>
                {
                    new HorarioFuncionamento(horarioFuncionamentoComando)
                };

                horarioFuncionamentoRepositorioMock.ListarAsync(Arg.Any<Expression<Func<HorarioFuncionamento, bool>>>(), Arg.Any<CancellationToken>())
                    .Returns(horariosExistentes);
                horarioFuncionamentoRepositorioMock.InserirAsync(Arg.Any<IEnumerable<HorarioFuncionamentoComando>>(), 
                                                                 Arg.Any<CancellationToken>())
                                                   .Returns(callInfo => callInfo.Arg<IEnumerable<HorarioFuncionamentoComando>>()
                                                       .Select(x => new HorarioFuncionamento(x))
                                                       .ToList());

                var resultado = await sut.EditarAsync(comandoEditar, CancellationToken.None);

                resultado.Should().NotBeNull();
                var resultadoLista = resultado.ToList();
                resultadoLista.Should().ContainSingle(x => x.DiaSemana == DiaSemanaEnum.Domingo);
                var horarioSegunda = resultadoLista.Should().ContainSingle(x => x.DiaSemana == DiaSemanaEnum.Segunda).Which;
                horarioSegunda.EmpresaId.Should().Be(horarioFuncionamentoComando.EmpresaId);
                horarioSegunda.HoraAbertura.Should().Be(comandoEditar.First().HoraAbertura);
                horarioSegunda.HoraFechamento.Should().Be(comandoEditar.First().HoraFechamento);
            }

            [Fact]
            public async Task Quando_EditarAsyncForChamadoComEmpresasDiferentes_Espero_ArgumentException()
            {
                var comandoEditar = new List<HorarioFuncionamentoEditarComando>
                {
                    new HorarioFuncionamentoEditarComando
                    {
                        EmpresaId = horarioFuncionamentoComando.EmpresaId,
                        DiaSemana = DiaSemanaEnum.Segunda,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento
                    },
                    new HorarioFuncionamentoEditarComando
                    {
                        EmpresaId = 999,
                        DiaSemana = DiaSemanaEnum.Terca,
                        HoraAbertura = horarioFuncionamentoComando.HoraAbertura,
                        HoraFechamento = horarioFuncionamentoComando.HoraFechamento
                    }
                };

                var horariosExistentes = new List<HorarioFuncionamento>
                {
                    new HorarioFuncionamento(horarioFuncionamentoComando)
                };

                horarioFuncionamentoRepositorioMock.ListarAsync(Arg.Any<Expression<Func<HorarioFuncionamento, bool>>>(), 
                                                                Arg.Any<CancellationToken>())
                                                   .Returns(horariosExistentes);

                Func<Task> act = async () => await sut.EditarAsync(comandoEditar, CancellationToken.None);

                await act.Should().ThrowAsync<ArgumentException>().WithMessage("Todos os horários devem pertencer à mesma empresa!");
            }
        }
    }
}
