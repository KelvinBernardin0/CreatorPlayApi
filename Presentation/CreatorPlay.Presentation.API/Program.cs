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

builder.Services.AddCustomFramework();
builder.Services.AddCustomOpenAPI();
builder.Services.AddCustomAuthenticationAPI();
builder.Services.AddApplication();
builder.Services.AddAuthorization();
builder.Services.AddPersistence();
builder.Host.UseSerilog();

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

app.UseSerilogRequestLogging();
app.UseHsts();
app.UseRouting();
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
	  .AllowAnyMethod()
	  .AllowAnyOrigin());

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/{**path}", async context => await context.Response.WriteAsync("Acesse url/docs (Ex. https://localhost:6584/docs)"));

app.Run();
