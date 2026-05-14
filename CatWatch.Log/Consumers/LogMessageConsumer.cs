using System.Text;
using System.Text.Json;
using CatWatch.Contracts.Messages;
using CatWatch.Contracts.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CatWatch.Log.Consumers;

public class LogMessageConsumer : BackgroundService
{
    private readonly ILogger<LogMessageConsumer> _logger;
    private readonly IConfiguration _config;


    public LogMessageConsumer(ILogger<LogMessageConsumer> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }


    public Task ProcessMessageAsync(LogMessage message)
    {
        _logger.LogInformation("{Level} - {Source}: {Message}", message.Level, message.Source, message.Message);
        return Task.CompletedTask;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = _config["RabbitMQ:HostName"] };

        using var connection = await factory.CreateConnectionAsync(stoppingToken);
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync("logs", durable: true, exclusive: false, autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = Encoding.UTF8.GetString(ea.Body.ToArray());
            var message = JsonSerializer.Deserialize<LogMessage>(body, JsonOptions.Default);
            if(message is null)
            {
                _logger.LogWarning("Received invalid log message: {Body}", body);
                return;
            }
            await ProcessMessageAsync(message);  
        };

        await channel.BasicConsumeAsync("logs", autoAck: true, consumer: consumer);
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
