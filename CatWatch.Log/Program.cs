using Serilog;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddHostedService<LogMessageConsumer>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.GrafanaLoki("http://loki:3100")
    .WriteTo.Console()
    .CreateLogger();

app.Run();
