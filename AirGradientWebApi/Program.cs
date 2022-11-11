using System.Text.Json.Serialization;
using AirGradientWebApi;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AirGradientWebApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AirGradientWebApiContext") ?? throw new InvalidOperationException("Connection string 'AirGradientWebApiContext' not found.")));

var app = builder.Build();

app.UsePathBase("/airgradient");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.MapGet("/test", () => {
    return Results.Ok("Test looks good.");
});

app.MapPost("/sensors/airgradient:{chipId}/measures", async (string chipId, AirGradientData data) => {
    return Results.Ok();
});

app.Run();

record AirGradientData(
    [property: JsonPropertyName("wifi")]
    string Wifi,
    [property: JsonPropertyName("rc02")]
    string? Co2,
    [property: JsonPropertyName("pm02")]
    string? Pm02,
    [property: JsonPropertyName("atmp")]
    string Temperature,
    [property: JsonPropertyName("rhum")]
    string? Humidity
    ) {
}
