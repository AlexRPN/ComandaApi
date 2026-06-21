using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EnderecoEmpresa> EnderecoEmpresas { get; set; }
        public DbSet<HorarioFuncionamento> HorariosFuncionamento { get; set; }
    }
}
