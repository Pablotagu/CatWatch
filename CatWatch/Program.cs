using CatWatch.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication;
using CatWatch.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();

