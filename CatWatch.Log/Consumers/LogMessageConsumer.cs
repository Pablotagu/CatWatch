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
    private readonly string _hostName;

    public LogMessageConsumer(ILogger<LogMessageConsumer> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
        _hostName = _config["RabbitMQ:HostName"] 
            ?? throw new InvalidOperationException("RabbitMQ:HostName configuration is missing.");

    }


    public Task ProcessMessageAsync(LogMessage message)
    {
        _logger.Log(message.Level,"{Source}: {LogMessage}", message.Source, message.Message);
        return Task.CompletedTask;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory { HostName = _hostName };

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
