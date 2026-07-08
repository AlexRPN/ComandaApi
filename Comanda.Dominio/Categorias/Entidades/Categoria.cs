using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Categorias.Entidades
{
    public class Categoria
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }

        // Relacionamento 1:N com Produto
        public ICollection<Produto> Produtos { get; private set; } = [];
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public AtivoInativoEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }

        private Categoria()
        {
            
        }

        public Categoria(CategoriaComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetNome(comando.Nome);
            SetDescricao(comando.Descricao);
            SetStatus(comando.Status);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException("O nome da categoria não pode ser nulo ou vazio!");
            }

            Nome = nome;
        }

        public void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentNullException("A descrição não pode ser nulo ou vazio!");
            }

            Descricao = descricao;
        }

        public void SetStatus(AtivoInativoEnum status)
        {
            Status = status;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            if(dataCadastro < DateTime.Now)
            {
                throw new ArgumentException("A data de cadastro não pode ser menor que a data atual!");
            }

            DataCadastro = dataCadastro;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            if(dataAlteracao < DateTime.Now)
            {
                throw new ArgumentException("A data de alteração não pode ser menor que a data atual!");
            }

            DataAlteracao = dataAlteracao;
        }
    }
}
