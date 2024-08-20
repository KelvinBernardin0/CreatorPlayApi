using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CreatorPlay.Persistence;

public class CreatorPlayContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, ICreatorPlayContext
{
	public CreatorPlayContext(DbContextOptions<CreatorPlayContext> options) : base(options) { }
	
	public CreatorPlayContext() { }

	public DatabaseFacade DataBaseOrigim => throw new NotImplementedException();

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
	{
		foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		{
			if (entry.Entity.CreatedAt == DateTime.MinValue)
			{
				entry.Entity.CreatedAt = DateTime.Now;
				entry.State = EntityState.Added;
			}
			else if (entry.State == EntityState.Modified)
				entry.Entity.ModifiedAt = DateTime.Now;
		}
		return await base.SaveChangesAsync(cancellationToken);
	}

	public IExecutionStrategy CreateExecutionStrategy() => Database.CreateExecutionStrategy();

	public void SetModifiedState<T>(T entity) => base.Entry(entity).State = EntityState.Modified;

	public void AttachModelToContext<T>(T entity) => base.Attach(entity);

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CreatorPlayContext).Assembly);
		base.OnModelCreating(modelBuilder);
	}

	public DbSet<ApplicationRole> ApplicationRole { get; set; }
	public DbSet<ApplicationUser> ApplicationUser { get; set; }
	public DbSet<LogActivity> LogActivity { get; set; }
}
