using Comanda.Dominio.Utils.Enumeradores;

namespace Comanda.Dominio.Adicionais.Comandos
{
    public class AdicionalComando
    {
        public int GrupoAdicionalId { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
