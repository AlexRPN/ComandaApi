
using Comanda.Dominio.Mesas.Entidades;
using Comanda.Dominio.Mesas.Repositorios.Interfaces;
using Comanda.Infra.Data;
using Comanda.Infra.Genericos;

namespace Comanda.Infra.Mesas.Repositorio
{
    public class MesaRepositorio : GenericoRepositorio<Mesa>, IMesaRepositorio
    {
        public MesaRepositorio(AppDbContext appDbContext) : base(appDbContext)
        {
            
        }
    }
}
