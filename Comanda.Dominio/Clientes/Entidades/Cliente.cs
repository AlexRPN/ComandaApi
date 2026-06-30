using Comanda.Dominio.Clientes.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Clientes.Entidades
{
    public class Cliente
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public int PontosFidelidade { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }
        public AtivoInativoEnum Status { get; private set; }

        private Cliente()
        {
            
        }

        public Cliente(ClienteComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetNome(comando.Nome);
            SetTelefone(comando.Telefone);
            SetPontosFidelidade(comando.PontosFidelidade);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
            SetStatus(comando.Status);
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome do cliente não pode ser nulo ou vazio.");
            }

            Nome = nome;
        }

        public void SetTelefone(string telefone)
        {
            if(string.IsNullOrWhiteSpace(telefone))
            {
                throw new ArgumentException("O telefone do cliente não pode ser nulo ou vazio.");
            }

            if(telefone.Length > 15)
            {
                throw new ArgumentException("O telefone do cliente não pode ter mais de 15 caracteres.");
            }

            Telefone = telefone;
        }

        public void SetPontosFidelidade(int pontosFidelidade)
        {
            PontosFidelidade = pontosFidelidade;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            DataCadastro = dataCadastro;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            DataAlteracao = dataAlteracao;
        }

        public void SetStatus(AtivoInativoEnum status)
        {
            Status = status;
        }
    }
}
