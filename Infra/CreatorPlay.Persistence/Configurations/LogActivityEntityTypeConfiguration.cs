using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public class LogActivityEntityTypeConfiguration : IEntityTypeConfiguration<LogActivity>
{
	public void Configure(EntityTypeBuilder<LogActivity> builder)
	{
		builder.ToTable(nameof(LogActivity));
		builder.HasKey(b => b.Id).IsClustered();
		builder.Property(b => b.LogLevel).IsRequired();
		builder.Property(b => b.User).IsRequired().HasColumnType("varchar(100)");
		builder.Property(b => b.CreatedAt).IsRequired();
		builder.Property(b => b.TypeUser).IsRequired().HasColumnType("varchar(20)"); ;
		builder.Property(b => b.Message).IsRequired().HasColumnType("nvarchar(max)");
	}
}