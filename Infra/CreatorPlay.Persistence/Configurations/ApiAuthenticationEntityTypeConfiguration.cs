using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CreatorPlay.Domain.Entities;

namespace CreatorPlay.Persistence.Configurations;

public class ApiAuthenticationEntityTypeConfiguration : IEntityTypeConfiguration<ApiAuthentication>
{
	public void Configure(EntityTypeBuilder<ApiAuthentication> builder)
	{
		builder.ToTable(nameof(ApiAuthentication));
		builder.HasKey(b => b.Id).IsClustered();
		builder.Property(b => b.Name).IsRequired().HasColumnType("nvarchar(150)");
		builder.Property(b => b.ClientId).IsRequired().HasColumnType("nvarchar(450)");
		builder.Property(b => b.Secret).IsRequired().HasColumnType("nvarchar(450)");
		builder.Property(b => b.AuthenticationEndpoint).IsRequired().HasColumnType("nvarchar(150)"); ;
		builder.Property(b => b.CreatedAt).IsRequired();
		builder.Property(b => b.ModifiedAt).IsRequired(false);
		builder.Property(b => b.Status).IsRequired();
	}
}