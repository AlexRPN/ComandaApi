using Comanda.Dominio.Empresas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Empresa> Empresas { get; set; }
    }
}
