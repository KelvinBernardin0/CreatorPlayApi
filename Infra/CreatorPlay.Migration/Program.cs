using CreatorPlay.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CreatorPlay.Persistence.Migration;

class Program
{
	public static async Task Main(string[] args)
	{
		Configuration.Build(Directory.GetCurrentDirectory());

		var loggerFactory = LoggerFactory.Create(builder =>
		{
			builder.AddConfiguration(Configuration.GetConfiguration().GetSection("Logging")).AddSimpleConsole();
		});
		
		var optionsBuilder = new DbContextOptionsBuilder<CreatorPlayContext>();
		optionsBuilder.UseLoggerFactory(loggerFactory)
					  .UseSqlServer(Configuration.ConnectionString);

		await new CreatorPlayContext(optionsBuilder.Options).Database.MigrateAsync();
	}
}
