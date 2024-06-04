using CreatorPlay.Application.Common.Interfaces;
using CreatorPlay.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CreatorPlay.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services)
	{
		services.AddHttpContextAccessor();
		services.AddDbContext<CreatorPlayContext>(options =>
		{
			options.UseSqlServer(Configuration.ConnectionString,
				sqlServerOptionsAction: sqlOptions =>
				{
					sqlOptions.EnableRetryOnFailure(
						maxRetryCount: 5,
						maxRetryDelay: TimeSpan.FromSeconds(10),
						errorNumbersToAdd: null);
				});
		},
			ServiceLifetime.Scoped
		);

		services.AddScoped<ICreatorPlayContext, CreatorPlayContext>();

		return services;
	}
}
