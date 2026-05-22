using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Domain.ValueObjects;
using CatWatch.Features.Readings.GetProbeReadings;
using NSubstitute;
using NSubstitute.ReturnsExtensions;


namespace CatWatch.Tests.Features.Readings.GetProbeReadings;


public class GetProbeReadingsHandlerTests
{
    private readonly IReadingRepository _readingRepository = Substitute.For<IReadingRepository>();
    private readonly IProbeRepository _probeRepository = Substitute.For<IProbeRepository>();


    [Fact]
    public async Task HandleAsync_ProbeNotFound_ThrowsNotFoundException()
    {
        var probeId = Guid.NewGuid();
        _probeRepository.GetByIdAsync(probeId, Arg.Any<CancellationToken>()).ReturnsNull();

        var handler = new GetProbeReadingsHandler(_readingRepository, _probeRepository);
        var command = new GetProbeReadingsQuery(probeId);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, Arg.Any<CancellationToken>()));
    }


    [Fact]
    public async Task HandleAsync_ProbeFound_ReturnsReadings()
    {
        var probeId = Guid.NewGuid();
        var probe = new Probe(probeId, "Test Probe");
        _probeRepository.GetByIdAsync(probeId, Arg.Any<CancellationToken>()).Returns(probe);

        var readings = new List<Reading>
        {
            new () 
            {
                ProbeId = Guid.NewGuid(), 
                Timestamp = DateTime.UtcNow, 
                Temperature = new Temperature(22.5)
            },
            new () 
            {
                ProbeId = Guid.NewGuid(), 
                Timestamp = DateTime.UtcNow.AddMinutes(-5), 
                Temperature = new Temperature(22.4)
            },
            new () 
            {
                ProbeId = Guid.NewGuid(), 
                Timestamp = DateTime.UtcNow.AddMinutes(-10), 
                Temperature = new Temperature(22.3)
            }

        };
        _readingRepository.GetByProbeIdAsync(probeId, Arg.Any<CancellationToken>()).Returns(readings);

        var handler = new GetProbeReadingsHandler(_readingRepository, _probeRepository);
        var command = new GetProbeReadingsQuery(probeId);

        var result = await handler.Handle(command, Arg.Any<CancellationToken>());

        Assert.Equal(readings, result);
    }
}