using Comanda.Dominio.Categorias.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comanda.Infra.Data.Configuracoes.Categorias
{
    public class CategoriaConfiguracao : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasOne(x => x.Empresa)
                   .WithMany(x => x.Categorias)
                   .HasForeignKey(x => x.EmpresaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Índice único composto: Uma empresa não pode ter duas categorias com o mesmo nome.
            builder.HasIndex(x => new { x.EmpresaId, x.Nome })
                   .IsUnique();
        }
    }
}
