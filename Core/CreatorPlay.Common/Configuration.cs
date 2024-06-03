using Microsoft.Extensions.Configuration;

namespace CreatorPlay.Common;

public static class Configuration
{
	public static IConfigurationRoot _configuration;

	public static void Build(string pathJsonFile)
	{
		var environment1 = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
		var environment2 = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		var builder = new ConfigurationBuilder()
			.SetBasePath(pathJsonFile)
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
			.AddJsonFile($"appsettings.{environment1}.json", optional: true, reloadOnChange: false)
			.AddJsonFile($"appsettings.{environment2}.json", optional: true, reloadOnChange: false)
			.AddJsonFile("secrets/appsettings.secrets.json", optional: true, reloadOnChange: false)
			.AddEnvironmentVariables();

		_configuration = builder.Build();
	}

	public static IConfiguration GetConfiguration()
	{
		return _configuration;
	}

	public static bool IsLocalhost => bool.Parse(_configuration.GetSection("AppSettings")["IsLocalhost"]);
	public static bool EnableSwagger => _configuration.GetSection("AppSettings")["EnableSwagger"] != null && bool.Parse(_configuration.GetSection("AppSettings")["EnableSwagger"]);
	public static string ConnectionString => _configuration.GetConnectionString("DefaultConnection");
	public static string ValidIssuer => _configuration.GetSection("JWT")["ValidIssuer"];
	public static string ValidAudience => _configuration.GetSection("JWT")["ValidAudience"];
	public static int Expiration => int.Parse(_configuration.GetSection("JWT")["Expiration"]);
	public static string Secret => _configuration.GetSection("JWT")["Secret"];
}
