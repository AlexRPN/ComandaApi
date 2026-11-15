using Comanda.Dominio.Pedidos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comanda.Infra.Data.Configuracoes.Pedidos
{
    public class PedidoConfiguracao : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            // Evita múltiplos caminhos de exclusão em cascata entre Empresa, Cliente e Pedido.
            builder.HasOne(x => x.Cliente)
                   .WithMany(x => x.Pedidos)
                   .HasForeignKey(x => x.ClienteId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
