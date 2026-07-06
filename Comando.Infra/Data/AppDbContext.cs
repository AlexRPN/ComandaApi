using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.EnderecoClientes.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.Usuarios.Entidades;
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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EnderecoCliente> EnderecoClientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
