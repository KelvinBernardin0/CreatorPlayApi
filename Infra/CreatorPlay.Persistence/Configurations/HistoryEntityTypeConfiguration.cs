using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public class HistoryEntityTypeConfiguration : IEntityTypeConfiguration<History>
{
	public void Configure(EntityTypeBuilder<History> builder)
	{
		builder.ToTable(nameof(History));

		builder.HasKey(h => h.HistoryId).IsClustered();
		builder.Property(h => h.UserId).IsRequired().HasColumnType("nvarchar(450)"); ;
		builder.Property(h => h.Description).IsRequired().HasColumnType("nvarchar(500)");
		builder.Property(h => h.CreatedAt).IsRequired();
		builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(h => h.UserId).OnDelete(DeleteBehavior.Cascade); 
		builder.HasOne<Team>().WithMany().HasForeignKey(h => h.TeamId).OnDelete(DeleteBehavior.SetNull); 
		builder.HasIndex(h => h.UserId);
		builder.HasIndex(h => h.TeamId);
	}
}
