
using Comanda.Dominio.Mesas.Comandos;
using Comanda.Dominio.Mesas.Entidades;

namespace Comanda.Dominio.Mesas.Servicos.Interfaces
{
    public interface IMesaServico
    {
        Task<Mesa> InserirAsync(MesaComando comando, CancellationToken cancellationToken);
    }
}
