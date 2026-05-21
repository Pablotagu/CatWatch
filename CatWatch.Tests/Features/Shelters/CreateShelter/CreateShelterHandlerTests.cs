
using CatWatch.Domain.Aggregates;
using CatWatch.Domain.Exceptions;
using CatWatch.Domain.Repositories;
using CatWatch.Features.Shelters.CreateShelter;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace CatWatch.Tests.Features.Shelters.CreateShelter;

public class CreateShelterHandlerTests
{
    private readonly IShelterRepository _shelterRepository = Substitute.For<IShelterRepository>();

    [Fact]
    public async Task HandleAsync_WhenShelterExists_ThrowsConflictException()
    {
        var shelter = new Shelter(Guid.NewGuid(), "Test Shelter");
        _shelterRepository.GetByNameAsync(shelter.Name, CancellationToken.None).Returns(shelter);

        var handler = new CreateShelterHandler(_shelterRepository);
        var command = new CreateShelterCommand(shelter.Name); 
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task HandleAsync_WhenShelterDoesNotExist_CreatesShelter()
    {
        var shelterName = "Test Shelter";
        _shelterRepository.GetByNameAsync(shelterName, CancellationToken.None).ReturnsNull();

        var handler = new CreateShelterHandler(_shelterRepository);
        var command = new CreateShelterCommand(shelterName);
        await handler.Handle(command, CancellationToken.None);

        await _shelterRepository.Received(1).AddAsync(Arg.Any<Shelter>(), CancellationToken.None);
    }
}