using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
