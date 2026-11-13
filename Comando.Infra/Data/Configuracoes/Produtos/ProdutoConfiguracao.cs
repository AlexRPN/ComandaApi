using Comanda.Dominio.Produtos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comanda.Infra.Data.Configuracoes.Produtos
{
    public class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Define a chave primária do produto para garantir a identidade única de cada registro.
            builder.HasKey(x => x.Id);

            // Configura a relação entre produto e empresa, preservando a integridade referencial sem exclusão em cascata.
            builder.HasOne(x => x.Empresa)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura a relação entre produto e categoria, evitando exclusão em cascata e mantendo as regras de negócio.
            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
