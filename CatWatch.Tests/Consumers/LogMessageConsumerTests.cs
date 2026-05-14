using CatWatch.Contracts.Messages;
using CatWatch.Log.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CatWatch.Tests.Consumers;

public class LogMessageConsumerTests
{
     [Fact]
    public async Task ProcessMessage_WhenValidMessage_LogsMessage()
    {
        var logger = Substitute.For<ILogger<LogMessageConsumer>>();
        var message = new LogMessage(LogPriority.Info, "Test message", DateTime.UtcNow, ServiceNames.CatWatch);
        var config = Substitute.For<IConfiguration>();
        config["RabbitMQ:HostName"].Returns("localhost");
        var consumer = new LogMessageConsumer(logger, config);

        await consumer.ProcessMessageAsync(message);

        logger.Received(1).Log(
            LogLevel.Information,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            null,
            Arg.Any<Func<object, Exception?, string>>());
     
    }
}