using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.Utils.Enumeradores;
using System.Collections;

namespace Comanda.Dominio.Produtos.Entidades
{
    public class Produto
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }

        // Relacionamento 1:N com Categoria
        public int CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; }

        public ICollection<GrupoAdicional> GrupoAdicional { get; set; } = [];
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int TempoPreparo { get; private set; }
        public AtivoInativoEnum Status { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataAlteracao { get; private set; }

        private Produto()
        {
            
        }

        public Produto(ProdutoComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetCategoriaId(comando.CategoriaId);
            SetNome(comando.Nome);
            SetDescricao(comando.Descricao);
            SetPreco(comando.Preco);
            SetTempoPreparo(comando.TempoPreparo);
            SetStatus(comando.Status);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetCategoriaId(int categoriaId)
        {
            CategoriaId = categoriaId;
        }

        public void SetNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException("O nome do produto não pode ser nulo ou vazio!");
            }

            Nome = nome;
        }

        public void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentNullException("A descrição do produto não pode ser nulo ou vazio!");
            }

            Descricao = descricao;
        }

        public void SetPreco(decimal preco)
        {
            if(preco < 0)
            {
                throw new ArgumentOutOfRangeException("O valor do produto não pode ser menor que zero!");
            }

            Preco = preco;
        }

        public void SetTempoPreparo(int tempoPreparo)
        {
            TempoPreparo = tempoPreparo;
        }

        public void SetStatus(AtivoInativoEnum status)
        {
            Status = status;
        }

        public void SetDataCadastro(DateTime dataCadastro)
        {
            if(dataCadastro < DateTime.Now)
            {
                throw new Exception("A data de cadastro não pode ser anterior a data atual!");
            }

            DataCadastro = dataCadastro;
        }

        public void SetDataAlteracao(DateTime dataAlteracao)
        {
            DataAlteracao = dataAlteracao;
        }
    }
}
