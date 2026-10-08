using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Categorias.Comandos
{
    public class CategoriaComando
    {
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
