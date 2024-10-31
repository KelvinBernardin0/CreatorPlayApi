using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public class TeamEntityTypeConfiguration : IEntityTypeConfiguration<Team>
{
	public void Configure(EntityTypeBuilder<Team> builder)
	{
		builder.ToTable(nameof(Team));
		builder.HasKey(b => b.TeamId).IsClustered();
		builder.Property(b => b.Name).IsRequired().HasColumnType("nvarchar(256)");
		builder.Property(b => b.LeaderId).IsRequired();
		builder.Property(b => b.CreatedAt).IsRequired();
		builder.Property(b => b.Status).IsRequired();
		builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(b => b.LeaderId).OnDelete(DeleteBehavior.Restrict);
		builder.HasIndex(b => b.LeaderId);
		builder.HasIndex(b => b.Status);
	}
}