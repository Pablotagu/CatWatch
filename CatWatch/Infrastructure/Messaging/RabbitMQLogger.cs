using CatWatch.Contracts.Messages;

namespace CatWatch.Infrastructure.Messaging;

public class RabbitMqLogger : ILogger
{
    private readonly IMessagePublisher _publisher;

    private readonly IConfiguration _config;


    public bool IsEnabled(LogLevel logLevel) => 
        logLevel >= Enum.Parse<LogLevel>(_config["RabbitMQ:MinimumLogLevel"] ?? "Information");


    public RabbitMqLogger(IMessagePublisher publisher, IConfiguration config)
    {
            _publisher = publisher;
            _config = config;
    }


    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;


    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
       
        _ = _publisher.PublishAsync(new LogMessage(logLevel, formatter(state, exception), DateTime.UtcNow, ServiceNames.CatWatch));
    }
}