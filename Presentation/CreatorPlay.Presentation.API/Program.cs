using CreatorPlay.Application;
using CreatorPlay.Common;
using CreatorPlay.Persistence;
using CreatorPlay.Presentation.API.Extensions;
using CreatorPlay.Presentation.API.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

Configuration.Build(Directory.GetCurrentDirectory());

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.SetBasePath(Directory.GetCurrentDirectory())
			 .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
			 .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Filter.ByExcluding(x => x.Level == LogEventLevel.Warning)
				.WriteTo.MSSqlServer(Configuration.ConnectionString,
					sinkOptions: new MSSqlServerSinkOptions
					{
						SchemaName = "dbo",
						AutoCreateSqlTable = true,
						TableName = "Logs"
					})
				.CreateLogger();

builder.Services.AddEncodingClassConfiguration();
builder.Services.AddCustomFramework();
builder.Services.AddCustomOpenAPI();
builder.Services.AddCustomAuthenticationAPI();
builder.Services.AddApplication();
builder.Services.AddAuthorization();
builder.Services.AddPersistence();
builder.Host.UseSerilog();

Log.Information("AllowOrigins:: {origins}", Configuration.OriginCors);
string originCors = GetAllowOriginCors(builder);
var app = builder.Build();

if (app.Environment.IsDevelopment() || Configuration.EnableSwagger)
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("../swagger/v1/swagger.json", "CreatorPlay v1");
		options.RoutePrefix = "docs";
		options.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
		{
			["activated"] = true
		};
	});
}

app.UseSerilogRequestLogging("HTTP {RequestMethod} {RequestPath} STATUS {StatusCode} IN {Elapsed:0.0000} ms");
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(originCors);
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapGet("/{**path}", async context => await context.Response.WriteAsync("Swagger desabilitado, contatar o administrador."));
	endpoints.MapControllers();
});

app.Run();

static string GetAllowOriginCors(WebApplicationBuilder builder)
{
	var originCors = "allowOrigins";
	builder.Services.AddCors(options =>
	{
		options.AddPolicy(name: originCors,
						  policy =>
						  {
							  if (Configuration.IsLocalhost)
								  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
							  else
								  policy.WithOrigins(Configuration.OriginCors).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
						  });
	});
	return originCors;
}