
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Mesas.Comandos;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Mesas.Entidades
{
    public class Mesa
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
        #endregion

        public int Id { get; private set; }
        public int Numero { get; private set; }
        public int Capacidade { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }
        public SituacaoMesaEnum Situacao { get; private set; }
        public StatusEnum Status { get; private set; }

        private Mesa()
        {
            
        }

        public Mesa(MesaComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetNumero(comando.Numero);
            SetCapacidade(comando.Capacidade);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
            SetSituacao(comando.Situacao);
            SetStatus(comando.Status);
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetNumero(int numero)
        {
            if(numero <= 0)
                throw new ArgumentException("O número da mesa deve ser maior que zero.");

            Numero = numero;
        }

        public void SetCapacidade(int capacidade)
        {
            if(capacidade <= 0)
                throw new ArgumentException("A capacidade da mesa deve ser maior que zero.");

            Capacidade = capacidade;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            if(dataCadastro < DateTime.Now.Date)
                throw new ArgumentException("A data de cadastro não pode ser anterior à data atual.");

            DataCadastro = dataCadastro;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            if(dataAlteracao < DateTime.Now.Date)
                throw new ArgumentException("A data de alteração não pode ser anterior à data atual.");

            DataAlteracao = dataAlteracao;
        }

        public void SetSituacao(SituacaoMesaEnum situacao)
        {
            Situacao = situacao;
        }

        public void SetStatus(StatusEnum status)
        {
            Status = status;
        }
    }
}
