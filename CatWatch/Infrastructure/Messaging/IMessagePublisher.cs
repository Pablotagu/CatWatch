namespace CatWatch.Infrastructure.Messaging;


public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default);
}