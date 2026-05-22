namespace CatWatch.Infrastructure.Messaging;

public class RabbitMqLoggerProvider : ILoggerProvider
{
    private readonly IMessagePublisher _publisher;
    private readonly IConfiguration _config;


    public RabbitMqLoggerProvider(IMessagePublisher publisher, IConfiguration config)
    {
        _publisher = publisher;
        _config = config;
    }


    public ILogger CreateLogger(string categoryName) => new RabbitMqLogger(_publisher, _config);
    
    
    public void Dispose()
    {
        
    }
}