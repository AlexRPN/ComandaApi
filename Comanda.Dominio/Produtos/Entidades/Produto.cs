using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.ImagensProdutos.Entidades;
using Comanda.Dominio.Produtos.Comandos;
using Comanda.Dominio.ProdutosVariacoes.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

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

        // Relacionamento 1:N com GrupoAdicional
        public ICollection<GrupoAdicional> GrupoAdicional { get; set; } = [];

        // Relacionamento 1:N com ImagemProduto
        public ICollection<ImagemProduto> ImagemProduto { get; set; } = [];

        // Relacionamento N:N com ProdutoVariacao
        public ICollection<ProdutoVariacao> ProdutoVariacao { get; set; } = [];
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int TempoPreparo { get; private set; }
        public AtivoInativoEnum Status { get; private set; }
        public SituacaoProdutoEnum SituacaoProduto { get; private set; }
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
            SetTempoPreparo(comando.TempoPreparo);
            SetStatus(comando.Status);
            SetSituacaoProduto(comando.SituacaoProduto);
            SetDataCadastro(comando.DataCadastro);
            SetDataAlteracao(comando.DataAlteracao);
        }

        public void SetSituacaoProduto(SituacaoProdutoEnum situacaoProduto)
        {
            SituacaoProduto = situacaoProduto;
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
