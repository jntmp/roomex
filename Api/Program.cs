using System.Text.Json.Serialization;
using Api.Extensions;
using Api.Services;
using Api.Services.Interfaces;

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

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
				app.UseSwagger();
				app.UseSwaggerUI();
		}

		// The default HTTP logging was too verbose and didn't include querysrtring
		// So I added middleware to log relevant request / response data
		app.UseCustomHttpLogging();

		app.MapControllers();

		app.Run();

	}
}

