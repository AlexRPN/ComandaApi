
using Comanda.Dominio.Mesas.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comanda.Infra.Data.Configuracoes.Mesas
{
    public class MesaConfiguracoes : IEntityTypeConfiguration<Mesa>
    {
        public void Configure(EntityTypeBuilder<Mesa> builder)
        {
            // Configuraçao para criar um índice único para a combinação de EmpresaId e Numero
            builder.HasIndex(x => new { x.EmpresaId, x.Numero })
               .IsUnique();
        }
    }
}
