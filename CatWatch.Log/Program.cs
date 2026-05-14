using CatWatch.Log.Consumers;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<LogMessageConsumer>();

builder.Host.UseSerilog((context, services, config) =>
{
    config
        .WriteTo.GrafanaLoki("http://loki:3100")
        .WriteTo.Console();
});

var app = builder.Build();
app.Run();
