using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Features.Probes.CreateProbe;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CatWatch.Tests.Features.Probes.CreateProbe;



public class CreateProbeHandlerTests
{
    private readonly IProbeRepository _probeRepository = Substitute.For<IProbeRepository>();
    private readonly IShelterRepository _shelterRepository = Substitute.For<IShelterRepository>();


    
    [Fact]
    public async Task HandleAsync_ShelterNotFound_ThrowsNotFoundException()
    {
        var shelterId = Guid.NewGuid();
        _shelterRepository.GetByIdAsync(shelterId, Arg.Any<CancellationToken>()).ReturnsNull();

        var handler = new CreateProbeHandler(_probeRepository, _shelterRepository);
        var command = new CreateProbeCommand("Test Probe", shelterId);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }


    [Fact]
    public async Task HandleAsync_ShelterFound_CreatesProbe()
    {
        var shelterId = Guid.NewGuid();
        var shelter = new Shelter(shelterId, "Test Shelter");
        _shelterRepository.GetByIdAsync(shelterId, Arg.Any<CancellationToken>()).Returns(shelter);

        var handler = new CreateProbeHandler(_probeRepository, _shelterRepository);
        var command = new CreateProbeCommand("Test Probe", shelterId);

        await handler.Handle(command, CancellationToken.None);

        await _probeRepository.Received(1).AddAsync(
            Arg.Is<Probe>(p => p.Name == command.Name && p.ShelterId == shelterId), Arg.Any<CancellationToken>());
    }
}