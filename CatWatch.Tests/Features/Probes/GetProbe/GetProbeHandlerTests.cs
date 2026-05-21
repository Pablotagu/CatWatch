using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Features.Probes.GetProbe;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CatWatch.Tests.Features.Probes.GetProbe;

public class GetProbeHandlerTests
{
    private readonly IProbeRepository _probeRepository = Substitute.For<IProbeRepository>();

    [Fact]
    public async Task HandleAsync_WhenProbeNotFound_ThrowsNotFoundException()
    {
        var probeId = Guid.NewGuid();
        _probeRepository.GetByIdAsync(probeId).ReturnsNull();

        var handler = new GetProbeHandler(_probeRepository);
        var query = new GetProbeQuery(probeId);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task HandleAsync_WhenProbeFound_ReturnsProbe()
    {
        var probe = new Probe(Guid.NewGuid(), "Test Probe");
        _probeRepository.GetByIdAsync(probe.Id).Returns(probe);

        var handler = new GetProbeHandler(_probeRepository);
        var query = new GetProbeQuery(probe.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Equal(probe, result);
    }
}