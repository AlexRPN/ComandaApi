using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.DataTransfer.Categorias.Response
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public AtivoInativoEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Mensagem { get; set; }
    }
}
