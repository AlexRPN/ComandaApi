using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Categorias.Comandos
{
    public class CategoriaComando
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public AtivoInativoEnum Status { get; set; }
    }
}
