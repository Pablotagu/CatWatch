using System.Text;
using System.Text.Json;
using CatWatch.Contracts.Serialization;
using RabbitMQ.Client;


namespace CatWatch.Infrastructure.Messaging;

public class RabbitMqPublisher : IMessagePublisher, IAsyncDisposable
{
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IChannel? _channel;


    public RabbitMqPublisher(IConfiguration config) => _config = config;


    private async Task EnsureConnectionAsync(CancellationToken cancellationToken)
    {
        if (_channel != null) 
            return;

        var factory = new ConnectionFactory { HostName = _config["RabbitMQ:HostName"] };
        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync();
        await _channel.QueueDeclareAsync("logs", durable: true, exclusive: false, autoDelete: false);
    }


    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        await EnsureConnectionAsync(cancellationToken);

        if (_channel == null)
        {
            throw new InvalidOperationException("RabbitMQ channel is not initialized.");
        }

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message, JsonOptions.Default));
        await _channel.BasicPublishAsync("", "logs", body, cancellationToken);
    }
    

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.DisposeAsync();
        }

        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }
    }
}