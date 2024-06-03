using Microsoft.EntityFrameworkCore;

namespace CreatorPlay.Persistence;

public class CreatorPlayContextFactory : DesignTimeDbContextFactoryBase<CreatorPlayContext>
{
	protected override CreatorPlayContext CreateNewInstance(DbContextOptions<CreatorPlayContext> options)
	{
		return new CreatorPlayContext(options);
	}
}
