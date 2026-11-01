
using Comanda.Dominio.HorariosFuncionamento.Comando;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.HorariosFuncionamentos.Entidades
{
    public class HorariosFuncionamentosTestes
    {
        private readonly HorarioFuncionamento sut;
        public HorariosFuncionamentosTestes()
        {
            sut = Builder<HorarioFuncionamento>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new HorarioFuncionamentoComando
                {
                    EmpresaId = 1,
                    DiaSemana = DiaSemanaEnum.Domingo,
                    HoraAbertura = DateTime.Today.AddHours(18),
                    HoraFechamento = DateTime.Today.AddHours(23),
                    Status = StatusEnum.Ativo
                };

                var horarioFuncionamento = new HorarioFuncionamento(comando);

                Assert.Equal(comando.EmpresaId, horarioFuncionamento.EmpresaId);
                Assert.Equal(comando.DiaSemana, horarioFuncionamento.DiaSemana);
                Assert.Equal(comando.HoraAbertura, horarioFuncionamento.HoraAbertura);
                Assert.Equal(comando.HoraFechamento, horarioFuncionamento.HoraFechamento);
                Assert.Equal(comando.Status, horarioFuncionamento.Status);
            }
        }

        public class SetEmpresaId : HorariosFuncionamentosTestes
        {
            [Fact]
            public void Quando_EmpresaIdForValido_Espero_PropriedadeAtualizada()
            {
                var empresaId = sut.EmpresaId;

                sut.SetEmpresaId(empresaId);

                Assert.Equal(empresaId, sut.EmpresaId);
            }
        }

        public class SetDiaSemana : HorariosFuncionamentosTestes
        {
            [Fact]
            public void Quando_DiaSemanaForValido_Espero_PropriedadeAtualizada()
            {
                var diaSemana = sut.DiaSemana;

                sut.SetDiaSemana(diaSemana);

                Assert.Equal(diaSemana, sut.DiaSemana);
            }
        }

        public class SetHoraAbertura : HorariosFuncionamentosTestes
        {
            [Fact]
            public void Quando_HoraAberturaForValido_Espero_PropriedadeAtualizada()
            {
                var horaAbertura = sut.HoraAbertura;

                sut.SetHoraAbertura(horaAbertura);

                Assert.Equal(horaAbertura, sut.HoraAbertura);
            }
        }

        public class SetHoraFechamento : HorariosFuncionamentosTestes
        {
            [Fact]
            public void Quando_HoraFechamentoForValido_Espero_PropriedadeAtualizada()
            {
                var horaFechamento = sut.HoraFechamento;

                sut.SetHoraFechamento(horaFechamento);

                Assert.Equal(horaFechamento, sut.HoraFechamento);
            }
        }
    }
}
