using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
{
	public virtual void Configure(EntityTypeBuilder<TBase> builder)
	{
		builder.Property<DateTime>(b => b.CreatedAt)
		   .UsePropertyAccessMode(PropertyAccessMode.Field)
		   .IsRequired(true);

		builder.Property<DateTime?>(b => b.ModifiedAt)
		   .UsePropertyAccessMode(PropertyAccessMode.Field)
		   .IsRequired(false);
	}
}
