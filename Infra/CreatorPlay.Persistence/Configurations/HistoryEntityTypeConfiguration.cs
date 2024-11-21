using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public class HistoryEntityTypeConfiguration : IEntityTypeConfiguration<History>
{  
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.ToTable(nameof(History));
        builder.HasKey(h => h.Id).IsClustered();
        builder.Property(h => h.UserId).IsRequired().HasColumnType("nvarchar(450)"); ;
        builder.Property(h => h.Description).IsRequired().HasColumnType("nvarchar(500)");
        builder.Property(h => h.CreatedAt).IsRequired();

        // Relacionamento com a tabela Team - usando a chave estrangeira TeamId
        builder.HasOne<Team>()
               .WithMany()
               .HasForeignKey(h => h.TeamId)  // Corrigido: chave estrangeira TeamId
               .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento com a tabela ApplicationUser (usuário) - usando a chave estrangeira UserId
        builder.HasOne<ApplicationUser>()
               .WithMany(u => u.Histories)
               .HasForeignKey(h => h.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(h => h.UserId);
        builder.HasIndex(h => h.Id);
    }

}
