using CatWatch.Domain.Repositories;
using CatWatch.Features.Probes.AddReading;
using CatWatch.Features.Probes.GetProbe;
using CatWatch.Infrastructure.Auth;
using CatWatch.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiKeyPolicy", policy =>
        policy.AddRequirements(new ApiKeyRequirement()));
});

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

builder.Services.AddSingleton<IAuthorizationHandler, ApiKeyHandler>();

builder.Services.AddScoped<AddReadingHandler>();
builder.Services.AddScoped<GetProbeHandler>();

builder.Services.AddScoped<IProbeRepository, ProbeRepository>();
builder.Services.AddScoped<IReadingRepository, ReadingRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

