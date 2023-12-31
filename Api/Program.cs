using System.Text.Json.Serialization;
using Api.Calculators;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDistanceCalculator, DistanceCalculator>();

builder.Services.AddControllers().AddJsonOptions(options => 
{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddLogging(builder =>
{
		builder.AddConsole();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomHttpLogging();

app.MapControllers();

app.Run();

