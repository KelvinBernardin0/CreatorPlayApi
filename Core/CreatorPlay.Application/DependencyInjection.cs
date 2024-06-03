using CreatorPlay.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CreatorPlay.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		return services;
	}
}
