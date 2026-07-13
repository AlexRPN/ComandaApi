using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.GrupoAdicionais.Comandos;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;

namespace Comanda.Dominio.GrupoAdicionais.Entidades
{
    public class GrupoAdicional
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1xN com Empresa
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }

        // Relacionamento 1xN com Adicional
        public ICollection<Adicional> Adicionais { get; private set; }

        // Tabela intermediária para o relacionamento N:N entre Produto e GrupoAdicional
        public ICollection<ProdutoGrupoAdicional> ProdutosGruposAdicionais { get; private set; } = [];
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }

        private GrupoAdicional()
        {

        }

        public GrupoAdicional(GrupoAdicionalComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetNome(comando.Nome);
        }

        public void SetEmpresaId(int empresaId)
        {
            EmpresaId = empresaId;
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentNullException("O Adicional não pode ser nulo ou vazio!");
            }

            Nome = nome;
        }
    }
}
