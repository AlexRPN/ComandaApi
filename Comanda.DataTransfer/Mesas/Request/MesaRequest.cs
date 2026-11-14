
using System.ComponentModel.DataAnnotations;

namespace Comanda.DataTransfer.Mesas.Request
{
    public class MesaRequest
    {
        [Required(ErrorMessage = "O ID da empresa é obrigatório!")]
        public int EmpresaId { get; set; }
        [Required(ErrorMessage = "O número da mesa é obrigatório!")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "A capacidade da mesa é obrigatória!")]
        public int Capacidade { get; set; }
    }
}
