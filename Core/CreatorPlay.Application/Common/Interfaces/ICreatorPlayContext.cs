using CreatorPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CreatorPlay.Application.Common.Interfaces;

public interface ICreatorPlayContext
{
	DbSet<ApplicationRole> ApplicationRole { get; set; }
	DbSet<ApplicationUser> ApplicationUser { get; set; }
    DbSet<GlobalConfiguration> GlobalConfiguration { get; set; }
	DbSet<Domain.Entities.LogActivity> LogActivity { get; set; }

    IExecutionStrategy CreateExecutionStrategy();
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	void SetModifiedState<T>(T entity);
	void AttachModelToContext<T>(T entity);
	DatabaseFacade DataBaseOrigim { get; }
}
