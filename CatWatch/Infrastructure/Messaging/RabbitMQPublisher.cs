using System.Text;
using System.Text.Json;
using CatWatch.Contracts.Serialization;
using RabbitMQ.Client;


namespace CatWatch.Infrastructure.Messaging;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly IConfiguration _config;

    public RabbitMqPublisher(IConfiguration config) => _config = config;

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        var factory = new ConnectionFactory { HostName = _config["RabbitMQ:HostName"] };
        using var connection = await factory.CreateConnectionAsync(cancellationToken);
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync("logs", durable: true, exclusive: false, autoDelete: false);
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, JsonOptions.Default));
        await channel.BasicPublishAsync("", "logs", body, cancellationToken);
    }
}