using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreatorPlay.Domain.Entities;

namespace CreatorPlay.Persistence.Configurations;

public class GlobalConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<GlobalConfiguration>
{
	public void Configure(EntityTypeBuilder<GlobalConfiguration> builder)
	{
		builder.ToTable(nameof(GlobalConfiguration));
		builder.HasKey(b => b.Id).IsClustered();
		builder.Property(b => b.EmailLogin).HasColumnType("varchar(150)");
		builder.Property(b => b.EmailPassword).HasColumnType("nvarchar(max)");
	}
}