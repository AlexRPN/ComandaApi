using Comanda.Dominio.Categorias.Comandos;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Categorias.Entidades
{
    public class Categoria
    {
        #region Navegação com os relacionamentos
        // Relacionamento 1:N com Empresa
        public int EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }
        #endregion

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public AtivoInativoEnum Status { get; private set; }

        private Categoria()
        {
            
        }

        public Categoria(CategoriaComando comando)
        {
            SetEmpresaId(comando.EmpresaId);
            SetNome(comando.Nome);
            SetDescricao(comando.Descricao);
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
    }
}
