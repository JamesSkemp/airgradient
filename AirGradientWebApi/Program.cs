using System.Text.Json;
using System.Text.Json.Serialization;
using AirGradientWebApi;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
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

app.MapGet("/test", ([FromServices]ILogger<Program> logger) => {
    logger.LogInformation("Test accessed");
    return Results.Ok("Test looks good.");
});

app.MapPost("/sensors/airgradient:{chipId}/measures", async (string chipId, AirGradientData data, [FromServices]ILogger<Program> logger) => {
    logger.LogInformation(chipId + " = " + JsonSerializer.Serialize(data));

    return Results.Ok();
});
app.MapPost("/asdf/sensors/airgradient:{chipId}/measures", async (string chipId, object data, [FromServices]ILogger<Program> logger) => {
    logger.LogInformation(JsonSerializer.Serialize(data));

    return Results.Ok();
});

app.Run();

record AirGradientData(
    [property: JsonPropertyName("wifi")]
    int Wifi,
    [property: JsonPropertyName("rco2")]
    int? Co2,
    [property: JsonPropertyName("pm02")]
    int? Pm02,
    [property: JsonPropertyName("atmp")]
    decimal Temperature,
    [property: JsonPropertyName("rhum")]
    int? Humidity
    ) {
}
