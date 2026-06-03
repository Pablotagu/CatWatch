using CatWatch.Log.Consumers;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<LogMessageConsumer>();

builder.Host.UseSerilog((context, services, config) =>
{
    config
        .WriteTo.GrafanaLoki("http://loki:3100", labels:
        [
            new LokiLabel { Key = "app", Value = "catwatch" },
            new LokiLabel { Key = "environment", Value = builder.Environment.EnvironmentName.ToLower() }
        ])
        .WriteTo.Console();

});

var app = builder.Build();
app.Run();
