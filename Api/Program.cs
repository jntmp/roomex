using System.Text.Json.Serialization;
using Api.Extensions;
using Api.Services;
using Api.Services.Interfaces;
using Prometheus;

namespace Api;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddSingleton<IDistanceCalculator, DistanceCalculator>();
		builder.Services.AddSingleton<ILocaleService, LocaleService>();
		builder.Services.AddScoped<IDistanceService, DistanceService>();

		builder.Services.AddControllers().AddJsonOptions(options => 
		{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});

		builder.Services.AddLogging(builder =>
		{
				// Under the assumption that this is a cloud service
				// I found it ok to log to console, since we can configure 
				// a cloud utility to scrape these consoles to ship to grafana etc.
				builder.AddConsole();
		});

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddAutoMapper(typeof(ApiMappingProfile));

		// Define a sample health check that always signals healthy state.
		// Purely to indicate the service is running for kubernetes
		builder.Services.AddHealthChecks()
			.AddCheck<HealthCheck>(nameof(HealthCheck));

		builder.Services.UseHttpClientMetrics();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
				app.UseSwagger();
				app.UseSwaggerUI();
		}

		// The default HTTP logging was too verbose and didn't include querysrtring
		// So I added middleware to log relevant request / response data
		app.UseMiddleware<HttpLoggingMiddleware>();
		
		// handle unexpected errors for now with middleware,
		// which should be mostly environmental in the current context 
		app.UseMiddleware<ErrorHandlerMiddleware>();

		app.MapControllers();

		app.UseRouting();

		// expose prometheus http metrics to ship to grafana
		app.UseHttpMetrics();
		app.UseEndpoints(endpoints => 
		{
			endpoints.MapMetrics();
		});

		// for kubernetes readiness/liveness probes
		app.MapHealthChecks("/health");

		app.Run();

	}
}
