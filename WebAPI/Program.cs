using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Adding Options
builder.Services.AddOptions(builder.Configuration);

// Adding HTTP CLient
builder.Services.ConfigureHttpClient();

// Adding Mappers
builder.Services.ConfigureAutoMapping();

// Configuring DB abd Auth
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);

// Injecting Services
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
