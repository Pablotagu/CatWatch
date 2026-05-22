using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Repositories;
using CatWatch.Domain.ValueObjects;
using CatWatch.Features.Readings.GetLatestReadings;
using NSubstitute;

namespace CatWatch.Tests.Features.Readings.GetLatestReadings;


public class GetLatestReadingsTests
{
    
    private readonly IReadingRepository _readingRepository = Substitute.For<IReadingRepository>();

    [Fact]
    public async Task HandleAsync_GetLatestsReadings_ReturnsReadings()
    {
        var readings = new List<Reading>
        {
            new() 
            {
                ProbeId = Guid.NewGuid(), 
                Temperature = new Temperature(22.5),
                Timestamp = DateTime.UtcNow
            },
            new() 
            {
                ProbeId = Guid.NewGuid(), 
                Temperature = new Temperature(23.0),
                Timestamp = DateTime.UtcNow
            }
        };

        var handler = new GetLatestReadingsHandler(_readingRepository);
        var command = new GetLatestReadingsQuery();
        _readingRepository.GetLatestReadingsAsync(Arg.Any<CancellationToken>()).Returns(readings);
        
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(readings, result);
    }
}

