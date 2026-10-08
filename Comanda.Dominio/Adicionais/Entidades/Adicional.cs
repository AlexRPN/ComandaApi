using Comanda.Dominio.Adicionais.Comandos;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Adicionais.Entidades
{
    public class Adicional
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com GrupoAdicional
        public GrupoAdicional GrupoAdicional { get; private set; }
        public int GrupoAdicionalId { get; private set; }
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public StatusEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }

        private Adicional()
        {

        }

        public Adicional(AdicionalComando comando)
        {
            SetGrupoAdicionalId(comando.GrupoAdicionalId);
            SetNome(comando.Nome);
            SetValor(comando.Valor);
            SetStatus(comando.Status);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetGrupoAdicionalId(int grupoAdicionalId)
        {
            GrupoAdicionalId = grupoAdicionalId;
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do adicional não pode ser vazio.");

            Nome = nome;
        }

        public void SetValor(decimal valor)
        {
            if (valor < 0)
                throw new ArgumentException("Valor do adicional não pode ser negativo.");

            Valor = valor;
        }

        public void SetStatus(StatusEnum status)
        {
            Status = status;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            DataCadastro = dataCadastro;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            DataAlteracao = dataAlteracao;
        }
    }
}
