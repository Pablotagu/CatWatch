using CatWatch.Contracts.Messages;
using CatWatch.Log.Consumers;
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
        var consumer = new LogMessageConsumer(logger);

        await consumer.ProcessMessageAsync(message);

        logger.Received(1).Log(
            LogLevel.Information,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            null,
            Arg.Any<Func<object, Exception?, string>>());
     
    }
}