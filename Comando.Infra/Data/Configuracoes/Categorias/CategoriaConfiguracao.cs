using Comanda.Dominio.Categorias.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comanda.Infra.Data.Configuracoes.Categorias
{
    public class CategoriaConfiguracao : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            // Define o relacionamento com a empresa para garantir que toda categoria pertença a uma empresa
            // e que a exclusão da empresa não remova categorias em cascata.
            builder.HasOne(x => x.Empresa)
                   .WithMany(x => x.Categorias)
                   .HasForeignKey(x => x.EmpresaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Cria um índice único composto para evitar duplicidade de nomes de categorias
            // dentro da mesma empresa.
            builder.HasIndex(x => new { x.EmpresaId, x.Nome })
                   .IsUnique();
        }
    }
}
