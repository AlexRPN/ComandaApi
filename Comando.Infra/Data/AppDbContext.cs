using Comanda.Dominio.Adicionais.Entidades;
using Comanda.Dominio.Categorias.Entidades;
using Comanda.Dominio.Clientes.Entidades;
using Comanda.Dominio.Empresas.Entidades;
using Comanda.Dominio.EnderecoClientes.Entidades;
using Comanda.Dominio.EnderecosEmpresas.Entidades;
using Comanda.Dominio.GrupoAdicionais.Entidades;
using Comanda.Dominio.HorariosFuncionamento.Entidades;
using Comanda.Dominio.ImagensProdutos.Entidades;
using Comanda.Dominio.Produtos.Entidades;
using Comanda.Dominio.ProdutosGruposAdicionais.Entidades;
using Comanda.Dominio.ProdutosVariacoes.Entidades;
using Comanda.Dominio.Usuarios.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Comanda.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EnderecoEmpresa> EnderecoEmpresas { get; set; }
        public DbSet<HorarioFuncionamento> HorariosFuncionamento { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EnderecoCliente> EnderecoClientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<GrupoAdicional> GruposAdicionais { get; set; }
        public DbSet<Adicional> Adicionais { get; set; }
        public DbSet<ImagemProduto> ImagensProdutos { get; set; }
        public DbSet<ProdutoVariacao> ProdutosVariacoes { get; set; }
        public DbSet<ProdutoGrupoAdicional> ProdutosGruposAdicionais { get; set; }
    }
}
