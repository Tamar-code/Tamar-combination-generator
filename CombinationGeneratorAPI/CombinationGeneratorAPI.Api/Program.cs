using CombinationGeneratorAPI.Api.Endpoints;
using CombinationGeneratorAPI.Application.Interfaces;
using CombinationGeneratorAPI.Application.Mapping;
using CombinationGeneratorAPI.Infrastructure.Services;
using NSwag.AspNetCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(CombinationGeneratorAPI.Application.Queries.GetCombinationsQuery).Assembly));
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CombinationMappingProfile>());
builder.Services.AddScoped<ICombinationService, CombinationService>();

// Add CORS with explicit configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Configure JSON to use camelCase (default in .NET but explicit for clarity)
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

// CORS middleware MUST be before routing
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.MapCombinationEndpoints();

app.Run();
