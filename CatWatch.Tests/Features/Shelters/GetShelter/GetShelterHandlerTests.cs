using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Features.Shelters.GetShelter;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CatWatch.Tests.Features.Shelters.GetShelter;


public class GetShelterHandlerTests
{
    private readonly IShelterRepository _shelterRepository = Substitute.For<IShelterRepository>();


    [Fact]
    public async Task HandleAsync_WhenShelterNotFound_ThrowsNotFoundException()
    {
        var shelterId = Guid.NewGuid();
        _shelterRepository.GetByIdAsync(shelterId, CancellationToken.None).ReturnsNull();

        var handler = new GetShelterHandler(_shelterRepository);
        var query = new GetShelterQuery(shelterId);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task HandleAsync_WhenShelterFound_ReturnsShelter()
    {
        var shelter = new Shelter(Guid.NewGuid(), "Test Shelter");
        _shelterRepository.GetByIdAsync(shelter.Id, CancellationToken.None).Returns(shelter);

        var handler = new GetShelterHandler(_shelterRepository);
        var query = new GetShelterQuery(shelter.Id);

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Equal(shelter, result);
    }
}