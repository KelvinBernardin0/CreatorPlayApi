using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public class TemplateHistoryEntityTypeConfiguration : IEntityTypeConfiguration<TemplateHistory>
{
	public void Configure(EntityTypeBuilder<TemplateHistory> builder)
	{
		builder.ToTable(nameof(TemplateHistory));
		builder.HasKey(b => b.Id).IsClustered();
		builder.Property(b => b.AspNetUsersId).IsRequired().HasColumnType("nvarchar(450)");
		builder.Property(b => b.Name).IsRequired().HasColumnType("varchar(150)");
		builder.Property(b => b.Template).IsRequired().HasColumnType("nvarchar(MAX)");
		builder.Property(b => b.Options).IsRequired().HasColumnType("nvarchar(MAX)");
		builder.Property(b => b.CreatedAt).IsRequired();
		builder.Property(b => b.ModifiedAt).IsRequired(false);
        builder.Property(b => b.TemplateStatus).IsRequired();
        builder.HasOne(b => b.ApplicationUser).WithMany(b => b.TemplateHistories).HasForeignKey(b => b.AspNetUsersId);
	}
}