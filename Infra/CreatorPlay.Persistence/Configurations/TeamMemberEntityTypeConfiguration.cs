using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatorPlay.Persistence.Configurations;

public class TeamMemberEntityTypeConfiguration : IEntityTypeConfiguration<TeamMember>
{
	public void Configure(EntityTypeBuilder<TeamMember> builder)
	{
		builder.ToTable(nameof(TeamMember));
		builder.HasKey(b => b.TeamMemberId).IsClustered();
		builder.Property(b => b.TeamId).IsRequired();
		builder.Property(b => b.UserId).IsRequired();
		builder.Property(b => b.IsLeader).IsRequired();
		builder.Property(b => b.CreatedAt).IsRequired();
		builder.Property(b => b.Status).IsRequired();
		builder.HasOne<Team>().WithMany(t => t.Members).HasForeignKey(b => b.TeamId).OnDelete(DeleteBehavior.Cascade); 
		builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Restrict);
		builder.HasIndex(b => new { b.TeamId, b.UserId }).IsUnique(); 
	}
}
