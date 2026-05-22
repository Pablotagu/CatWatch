using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Features.Readings.AddReading;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CatWatch.Tests.Features.Probes.AddReading;

public class AddReadingHandlerTests
{
    private readonly IProbeRepository _probeRepository = Substitute.For<IProbeRepository>();
    private readonly IReadingRepository _readingRepository = Substitute.For<IReadingRepository>();

    [Fact]
    public async Task HandleAsync_WhenProbeNotFound_ThrowsNotFoundException()
    {
        var probeId = Guid.NewGuid();
        _probeRepository.GetByIdAsync(probeId).ReturnsNull();

        var handler = new AddReadingHandler(_readingRepository, _probeRepository);
        var command = new AddReadingCommand(probeId, 22.5);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command));
    }

    [Fact]
    public async Task HandleAsync_WhenProbeFound_AddsReading()
    {
        var probe = new Probe(Guid.NewGuid(), "Test Probe");

        _probeRepository.GetByIdAsync(probe.Id).Returns(probe);

        var handler = new AddReadingHandler(_readingRepository, _probeRepository);
        var command = new AddReadingCommand(probe.Id, 22.5);

        await handler.Handle(command);

        await _readingRepository.Received(1).AddAsync(Arg.Is<Reading>(r =>
            r.ProbeId == probe.Id &&
            r.Temperature.Value == 22.5
        ));
    }
}
