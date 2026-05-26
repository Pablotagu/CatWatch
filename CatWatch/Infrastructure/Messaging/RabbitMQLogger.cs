using CatWatch.Contracts.Messages;

namespace CatWatch.Infrastructure.Messaging;

public class RabbitMqLogger : ILogger
{
    private readonly IMessagePublisher _publisher;
    private readonly LogLevel _minimumLevel;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minimumLevel;


    public RabbitMqLogger(IMessagePublisher publisher, IConfiguration config)
    {
            _publisher = publisher;
            _minimumLevel = Enum.TryParse<LogLevel>(config["RabbitMQ:MinimumLogLevel"], out var level) ? level : LogLevel.Information;    
    }


    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;


    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
       
        try
        {
            _ = _publisher.PublishAsync(new LogMessage(logLevel, formatter(state, exception), DateTime.UtcNow, ServiceNames.CatWatch));
        }
        catch (Exception ex)
        {
            // avoid unhandled exceptions
        }
    }
}