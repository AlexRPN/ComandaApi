
using Comanda.Dominio.Mesas.Comandos;
using Comanda.Dominio.Mesas.Entidades;
using Comanda.Dominio.Utils.Enumeradores;
using FizzWare.NBuilder;

namespace Comanda.Dominio.Testes.Mesas.Entidades
{
    public class MesasTestes
    {
        private readonly Mesa sut;
        public MesasTestes()
        {
            sut = Builder<Mesa>.CreateNew().Build();
        }

        public class Construtor
        {
            [Fact]
            public void Quando_ConstrutorForChamado_Espero_PropriedadesInicializadas()
            {
                var comando = new MesaComando
                {
                    EmpresaId = 1,
                    Numero = 1,
                    Capacidade = 4,
                    Situacao = SituacaoMesaEnum.Livre,
                    Status = StatusEnum.Ativo,
                    DataCadastro = DateTime.Now.Date,
                    DataAlteracao = DateTime.Now.Date
                };
                var mesa = new Mesa(comando);
                Assert.Equal(comando.EmpresaId, mesa.EmpresaId);
                Assert.Equal(comando.Numero, mesa.Numero);
                Assert.Equal(comando.Capacidade, mesa.Capacidade);
                Assert.Equal(comando.Situacao, mesa.Situacao);
                Assert.Equal(comando.Status, mesa.Status);
                Assert.Equal(comando.DataCadastro, mesa.DataCadastro);
                Assert.Equal(comando.DataAlteracao, mesa.DataAlteracao);
            }
        }

        public class SetEmpresaId : MesasTestes
        {
            [Fact]
            public void Quando_EmpresaIdForValido_Espero_PropriedadeAtualizada()
            {
                var empresaId = 1;
                sut.SetEmpresaId(empresaId);
                Assert.Equal(empresaId, sut.EmpresaId);
            }
        }

        public class SetNumero : MesasTestes
        {
            [Fact]
            public void Quando_NumeroForValido_Espero_PropriedadeAtualizada()
            {
                var numero = 1;
                sut.SetNumero(numero);
                Assert.Equal(numero, sut.Numero);
            }

            [Fact]
            public void Quando_NumeroForMenorQueZero_Espero_ArgumentException()
            {
                var numero = -3;

                Assert.Throws<ArgumentException>(() => sut.SetNumero(numero));
            }

            [Fact]
            public void Quando_NumeroForZero_Espero_ArgumentException()
            {
                var numero = 0;
                Assert.Throws<ArgumentException>(() => sut.SetNumero(numero));
            }
        }

        public class SetCapacidade : MesasTestes
        {
            [Fact]
            public void Quando_CapacidadeForValido_Espero_PropriedadeAtualizada()
            {
                var capacidade = 4;
                sut.SetCapacidade(capacidade);
                Assert.Equal(capacidade, sut.Capacidade);
            }

            [Fact]
            public void Quando_CapacidadeForMenorQueZero_Espero_ArgumentException()
            {
                var capacidade = -3;
                Assert.Throws<ArgumentException>(() => sut.SetCapacidade(capacidade));
            }

            [Fact]
            public void Quando_CapacidadeForZero_Espero_ArgumentException()
            {
                var capacidade = 0;
                Assert.Throws<ArgumentException>(() => sut.SetCapacidade(capacidade));
            }
        }

        public class SetDataCadastro : MesasTestes
        {
            [Fact]
            public void Quando_DataCadastroForValido_Espero_PropriedadeAtualizada()
            {
                var dataCadastro = DateTime.Now.Date;
                sut.SetDataCadastro(dataCadastro);
                Assert.Equal(dataCadastro, sut.DataCadastro);
            }
        }

        public class SetDataAlteracao : MesasTestes
        {
            [Fact]
            public void Quando_DataAlteracaoForValido_Espero_PropriedadeAtualizada()
            {
                var dataAlteracao = DateTime.Now.Date;
                sut.SetDataAlteracao(dataAlteracao);
                Assert.Equal(dataAlteracao, sut.DataAlteracao);
            }
        }

        public class SetSituacao : MesasTestes
        {
            [Fact]
            public void Quando_SituacaoForValido_Espero_PropriedadeAtualizada()
            {
                var situacao = SituacaoMesaEnum.Livre;
                sut.SetSituacao(situacao);
                Assert.Equal(situacao, sut.Situacao);
            }
        }

        public class SetStatus : MesasTestes
        {
            [Fact]
            public void Quando_StatusForValido_Espero_PropriedadeAtualizada()
            {
                var status = StatusEnum.Ativo;
                sut.SetStatus(status);
                Assert.Equal(status, sut.Status);
            }
        }
    }
}
